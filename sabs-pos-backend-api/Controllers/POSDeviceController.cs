using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using sabs_pos_backend_api.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sabs_pos_backend_api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/pos_devices")]
    public class POSDeviceController : ActionResultBase
    {
        readonly IActivityLogService _srvActivityLog;
        readonly IPOSDeviceService _srvPOSDevice;
        readonly IStoreService _srvStore;
        public POSDeviceController(IActivityLogService srvActivityLog, IPOSDeviceService srvPOSDevice, IStoreService srvStore)
        {
            _srvActivityLog = srvActivityLog;
            _srvPOSDevice = srvPOSDevice;
            _srvStore = srvStore;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Save([FromBody] POSDeviceReq req)
        {
            return await ExecuteAsync(async () =>
            {
                if (req.IsNull() || req.name.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "POS device name is required");

                if (req.store_id.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "Store ID is required");

                var store = await _srvStore.Get<stores>(QF.EQ("uuid", req.store_id));
                if (store.IsNull())
                    throw new ResponseException(ResponseCode.NOT_FOUND, $"Store '{req.store_id}' was not found");

                var pos_device_uuid = req.id.IsNull() ? await create(req, store.id) : await update(req, store.id);
                return await _srvPOSDevice.Get<POSDeviceRes>(QF.EQ("uuid", pos_device_uuid), include: QI.IC("stores", "uuid store_uuid"));
            });
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Read(string store_id, bool show_deleted = false)
        {
            return await ExecuteAsync(async () =>
            {
                var filters = new List<string>();

                // Add store id filter
                if (!string.IsNullOrEmpty(store_id))
                    filters.Add(QF.EQ("stores", "uuid", store_id));

                // Add deleted filter
                if (!show_deleted)
                    filters.Add(QF.EM("deleted_at"));

                return await _srvPOSDevice.Read<POSDeviceRes>(filters.Merge(), QS.DESC("id"), include: QI.IC("stores", "uuid store_uuid"));
            });
        }

        [HttpGet]
        [Route("{pos_device_id}")]
        public async Task<IActionResult> Get(string pos_device_id)
        {
            return await ExecuteAsync(async () =>
            {
                if (pos_device_id.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "POSDevice ID is required");

                var POSDevice = await _srvPOSDevice.Get<POSDeviceRes>(QF.EQ("uuid", pos_device_id), include: QI.IC("stores", "uuid store_uuid"));
                if (POSDevice.IsNull())
                    throw new ResponseException(ResponseCode.NOT_FOUND, $"POS device '{pos_device_id}' was not found");

                return POSDevice;
            });
        }

        [HttpDelete]
        [Route("{pos_device_id}")]
        public async Task<IActionResult> Delete(string pos_device_id)
        {
            return await ExecuteAsync(async () =>
            {
                if (pos_device_id.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "POSDevice ID is required");

                var POSDevice = await _srvPOSDevice.Get<pos_devices>(QF.EQ("uuid", pos_device_id));
                if (POSDevice.IsNull())
                    throw new ResponseException(ResponseCode.NOT_FOUND, $"POS device '{pos_device_id}' was not found");

                await _srvPOSDevice.Delete(POSDevice.id);

                await _srvActivityLog.Create(new activity_logs
                {
                    action_type = ActionType.POS_DEVICE_DELETED,
                    detail = string.Format(ActionFormat.POS_DEVICE_DELETED, login.name, POSDevice.name),
                    created_at = DateTime.Now,
                    created_by = login.id
                });

                return POSDevice.uuid;
            });
        }

        async Task<string> create(POSDeviceReq req, int? store_id)
        {
            var POSDevice = await _srvPOSDevice.Get<pos_devices>(QF.EQ("name", req.name));
            if (!POSDevice.IsNull())
                throw new ResponseException(ResponseCode.CONFLICTING_PARAMETERS, "POS device name is already in use");

            var newPOSDevice = new pos_devices
            {
                uuid = Guid.NewGuid().ToString(),
                name = req.name,
                store_id = store_id
            };
            newPOSDevice.activated = false;

            await _srvPOSDevice.Create(newPOSDevice);

            await _srvActivityLog.Create(new activity_logs
            {
                action_type = ActionType.POS_DEVICE_CREATED,
                detail = string.Format(ActionFormat.POS_DEVICE_CREATED, login.name, newPOSDevice.name),
                created_at = DateTime.Now,
                created_by = login.id
            });

            return newPOSDevice.uuid;
        }

        async Task<string> update(POSDeviceReq req, int? store_id)
        {
            var POSDevice = await _srvPOSDevice.Get<pos_devices>(QF.EQ("uuid", req.id));
            if (POSDevice.IsNull())
                throw new ResponseException(ResponseCode.NOT_FOUND, $"POS device '{req.id}' was not found");

            if (POSDevice.name != req.name)
            {
                var POSDeviceByName = await _srvPOSDevice.Get<pos_devices>(QF.EQ("name", req.name).Merge(QF.NEQ("uuid", req.id)));
                if (!POSDeviceByName.IsNull())
                    throw new ResponseException(ResponseCode.CONFLICTING_PARAMETERS, "POS device name is already in use");
            }

            POSDevice.name = req.name;
            POSDevice.store_id = store_id;

            await _srvPOSDevice.Update(POSDevice);

            await _srvActivityLog.Create(new activity_logs
            {
                action_type = ActionType.POS_DEVICE_UPDATED,
                detail = string.Format(ActionFormat.POS_DEVICE_UPDATED, login.name, POSDevice.name),
                created_at = DateTime.Now,
                created_by = login.id
            });

            return POSDevice.uuid;
        }
    }
}
