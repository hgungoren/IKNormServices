namespace Serendip.IK
{
    public static class EventNames
    {

        public static string KNORM_CREATED { get => $"{ModelTypes.KNORM}.created"; }
        public static string KNORM_STATUS_CHANGED { get => $"{ModelTypes.KNORM}.changed"; }
        public static string KNORM_REQUEST_END { get => $"{ModelTypes.KNORM}.end"; }
        public static string ALL_EVENT { get => "*"; }

    }

}
