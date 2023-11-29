using ParkBusinessLayer.Model;

namespace ParkBusinessLayer.Interfaces {
    public interface IParkRepository {
        
        void VoegParkToe(Park p);
        void UpdatePark(Park p);
        Park GeefPark(string id);
    }
}