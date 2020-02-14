//
// One CTA Station
//

namespace program.Models
{

  public class Station
	{
	
		// data members with auto-generated getters and setters:
        public int StationID { get; set; }
		public string StationName { get; set; }
		public int AvgDailyRidership { get; set; }
        public int NumStops { get; set; }
        public string HandicapAccessible { get; set; }
        public int NumRiders { get; set; }
	
		// default constructor:
		public Station()
		{ }
		
		// constructor:
		public Station(int id, string name, int avgDailyRidership, int numStops, string handicapAccessible, int numRiders)
		{
			StationID = id;
			StationName = name;
			AvgDailyRidership = avgDailyRidership;
            NumStops = numStops;
            HandicapAccessible = handicapAccessible;
            NumRiders = numRiders;
		}
		
	}//class

}//namespace