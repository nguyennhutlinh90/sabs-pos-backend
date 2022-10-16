using System.ComponentModel;

namespace sabs_pos_backend_api
{
    public enum ResponseCode
    {
        // HTTP errors
        [Description("Bad request")]
        BAD_REQUEST = 400,
        [Description("Unauthorized")]
        UNAUTHORIZED = 401,
        [Description("Payment required")]
        PAYMENT_REQUIRED = 402,
        [Description("Forbidden")]
        FORBIDDEN = 403,
        [Description("Not found")]
        NOT_FOUND = 404,
        [Description("Unsupported media type")]
        UNSUPPORTED_MEDIA_TYPE = 415,
        [Description("Rate limited")]
        RATE_LIMITED = 429,
        [Description("Internal server error")]
        INTERNAL_SERVER_ERROR = 500,

        // Custom errors
        [Description("Incorrect value type")]
        INCORRECT_VALUE_TYPE = 601,
        [Description("Missing required parameter")]
        MISSING_REQUIRED_PARAMETER = 602,
        [Description("Invalid value")]
        INVALID_VALUE = 603,
        [Description("Invalid range")]
        INVALID_RANGE = 604,
        [Description("Invalid cursor")]
        INVALID_CURSOR = 605,
        [Description("Conflicting parameters")]
        CONFLICTING_PARAMETERS = 606
    }
}
