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
    [Route("v{version:apiVersion}/stores")]
    public class StoreController : ActionResultBase
    {
        readonly IActivityLogService _srvActivityLog;
        readonly IStoreService _srvStore;
        public StoreController(IActivityLogService srvActivityLog, IStoreService srvStore)
        {
            _srvActivityLog = srvActivityLog;
            _srvStore = srvStore;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Save([FromBody] StoreReq req)
        {
            return await ExecuteAsync(async () =>
            {
                if (req.IsNull() || req.name.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "Store name is required");

                var store_uuid = req.id.IsNull() ? await create(req) : await update(req);
                return await _srvStore.Get<StoreRes>(QF.EQ("uuid", store_uuid), include: QI.IC("accounts", "uuid owner_uuid"));
            });
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Read(string store_ids = "", DateTime? created_at_min = null, DateTime? created_at_max = null, DateTime? updated_at_min = null, DateTime? updated_at_max = null, bool show_deleted = false)
        {
            return await ExecuteAsync(async () =>
            {
                var filters = new List<string>();

                // Add store IDs filter
                if (!string.IsNullOrEmpty(store_ids))
                    filters.Add(QF.IN("uuid", store_ids.Split(",")));

                // Add created date range filter
                if (created_at_min.HasValue)
                    filters.Add(QF.GTE("created_at", created_at_min.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                if (created_at_max.HasValue)
                {
                    if (created_at_min.HasValue)
                    {
                        if (created_at_max.Value.Subtract(created_at_min.Value).TotalMilliseconds < 0)
                            throw new ResponseException(ResponseCode.INVALID_RANGE, "Invalid created date range (created_at_max < created_at_min)");
                    }
                    filters.Add(QF.LTE("created_at", created_at_max.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                }

                // Add updated date range filter
                if (updated_at_min.HasValue)
                    filters.Add(QF.GTE("updated_at", created_at_min.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                if (updated_at_max.HasValue)
                {
                    if (updated_at_min.HasValue)
                    {
                        if (updated_at_max.Value.Subtract(updated_at_min.Value).TotalMilliseconds < 0)
                            throw new ResponseException(ResponseCode.INVALID_RANGE, "Invalid updated date range (updated_at_max < updated_at_min)");
                    }
                    filters.Add(QF.LTE("updated_at", created_at_max.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                }

                // Add deleted filter
                if (!show_deleted)
                    filters.Add(QF.EM("deleted_at"));

                return await _srvStore.Read<StoreRes>(filters.Merge(), QS.DESC("created_at"), include: QI.IC("accounts", "uuid owner_uuid"));
            });
        }

        [HttpGet]
        [Route("{store_id}")]
        public async Task<IActionResult> Get(string store_id)
        {
            return await ExecuteAsync(async () =>
            {
                if (store_id.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "Store ID is required");

                var store = await _srvStore.Get<StoreRes>(QF.EQ("uuid", store_id), include: QI.IC("accounts", "uuid owner_uuid"));
                if (store.IsNull())
                    throw new ResponseException(ResponseCode.NOT_FOUND, $"Store '{store_id}' was not found");

                return store;
            });
        }

        [HttpDelete]
        [Route("{store_id}")]
        public async Task<IActionResult> Delete(string store_id)
        {
            return await ExecuteAsync(async () =>
            {
                if (store_id.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "Store ID is required");

                var store = await _srvStore.Get<stores>(QF.EQ("uuid", store_id));
                if (store.IsNull())
                    throw new ResponseException(ResponseCode.NOT_FOUND, $"Store '{store_id}' was not found");

                await _srvStore.Delete(store.id);

                await _srvActivityLog.Create(new activity_logs
                {
                    action_type = ActionType.STORE_DELETED,
                    detail = string.Format(ActionFormat.STORE_DELETED, login.name, store.name),
                    created_at = DateTime.Now,
                    created_by = login.id
                });

                return store.uuid;
            });
        }

        async Task<string> create(StoreReq req)
        {
            var store = await _srvStore.Get<stores>(QF.EQ("name", req.name));
            if (!store.IsNull())
                throw new ResponseException(ResponseCode.CONFLICTING_PARAMETERS, "Store name is already in use");

            var newStore = req.MapTo<stores>();
            newStore.uuid = Guid.NewGuid().ToString();
            newStore.owner_id = login.owner_id;
            newStore.created_at = newStore.updated_at = DateTime.Now;

            await _srvStore.Create(newStore);

            await _srvActivityLog.Create(new activity_logs
            {
                action_type = ActionType.STORE_CREATED,
                detail = string.Format(ActionFormat.STORE_CREATED, login.name, newStore.name),
                created_at = DateTime.Now,
                created_by = login.id
            });

            return newStore.uuid;
        }

        async Task<string> update(StoreReq req)
        {
            var store = await _srvStore.Get<stores>(QF.EQ("uuid", req.id));
            if (store.IsNull())
                throw new ResponseException(ResponseCode.NOT_FOUND, $"Store '{req.id}' was not found");

            if (store.name != req.name)
            {
                var storeByName = await _srvStore.Get<stores>(QF.EQ("name", req.name).Merge(QF.NEQ("uuid", req.id)));
                if (!storeByName.IsNull())
                    throw new ResponseException(ResponseCode.CONFLICTING_PARAMETERS, "Store name is already in use");
            }

            store.name = req.name;
            store.address = req.address;
            store.phone_number = req.phone_number;
            store.description = req.description;
            store.updated_at = DateTime.Now;

            await _srvStore.Update(store);

            await _srvActivityLog.Create(new activity_logs
            {
                action_type = ActionType.STORE_UPDATED,
                detail = string.Format(ActionFormat.STORE_UPDATED, login.name, store.name),
                created_at = DateTime.Now,
                created_by = login.id
            });

            return store.uuid;
        }
    }
}
