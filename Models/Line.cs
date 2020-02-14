//
// One CTA Line
//

namespace program.Models
{

  public class Line
	{
	
		// data members with auto-generated getters and setters:
        public int LineID { get; set; }
		public string LineColor { get; set; }
        //public List<Models.Station> stationList {get; set; }
		// default constructor:
		public Line()
		{ }
		
		// constructor:
		public Line(int id, string color)
		{
			LineID = id;
			LineColor = color;
            
			
            /*AvgDailyRidership = avgDailyRidership;
            NumStops = numStops;
            HandicapAccessible = handicapAccessible;*/
		}
		
	}//class

}//namespace