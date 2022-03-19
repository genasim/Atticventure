class RoomService { 
    private static RoomService instance = null;
    // private GameObect[] allRooms = []; 
    private RoomService()
    {

    }

    RoomService roomservice = RoomService.Instance;

    public static RoomService Instance 
    {
        get{
            if(instance == null) {
                instance = new RoomService();
            }
            return instance;
        }
    }
}