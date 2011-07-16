
namespace ImageStoreSystem.Infrastructure
{
	public enum DrawMode
	{
		NoDrawing = 0,
		PointsOnly = 1,
		RectsOnly = 2
	}

	public interface IMapRegistrator
	{
		void RegisterMap(string regionName, string mapName, DrawMode mode);
		void RegisterMap(string regionName, DrawMode mode);
	}
}
