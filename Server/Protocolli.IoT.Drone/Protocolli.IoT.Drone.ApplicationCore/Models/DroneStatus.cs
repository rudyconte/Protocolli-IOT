namespace Protocolli.IoT.Drone.ApplicationCore.Models
{
    public class DroneStatus
    {
        public int DroneId { get; set; }
        public long Timestamp { get; set; }
        public Position Position { get; set; }
        public float Velocity { get; set; }
        public float Battery { get; set; }
    }
}
