using System;
namespace SignWaveTest {
	public interface IOccilator {
		double GetNext(int sampleNumberInSecond);
		void SetFrequency(double value);
	}

	public interface IDetuneable {
		void SetDetune(double maxDetune);
	}
}
