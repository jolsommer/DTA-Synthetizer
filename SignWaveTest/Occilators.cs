using System;
using System.Collections.Generic;
using System.Text;

namespace SignWaveTest {
	public class SineOccilator : SignWaveTest.IOccilator {

		private double _radiansPerCircle = Math.PI * 2;
		private double _currentFrequency = 2000;
		private double _sampleRate = 44100;

		public SineOccilator(double sampleRate) {
			_sampleRate = sampleRate;
		}

		public void SetFrequency(double value){
			_currentFrequency = value;
		}

		public double GetNext(int sampleNumberInSecond) {
			double samplesPerOccilation = (_sampleRate / _currentFrequency);
			double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;
			return Math.Sin( depthIntoOccilations * _radiansPerCircle);
		}
	}

	public class SquareOccilator : SignWaveTest.IOccilator {

		private double _radiansPerCircle = Math.PI * 2;
		private double _currentFrequency = 2000;
		private double _sampleRate = 44100;

		public SquareOccilator(double sampleRate) {
			_sampleRate = sampleRate;
		}

		public void SetFrequency(double value) {
			_currentFrequency = value;
		}

		public double GetNext(int sampleNumberInSecond) {
			
			double samplesPerOccilation = (_sampleRate / _currentFrequency);
			double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;

			double sinPart = Math.Sin(depthIntoOccilations * _radiansPerCircle);
			if (depthIntoOccilations > 0.5) {
				sinPart += 0.5;
			}
			else {
				sinPart -= 0.5;
			}

			return sinPart;
		}
	}

	public class SawToothOccilator : SignWaveTest.IOccilator {

		private double _radiansPerCircle = Math.PI * 2;
		private double _currentFrequency = 2000;
		private double _sampleRate = 44100;
		private double _detune = 0.0;
	
		public SawToothOccilator(double sampleRate) {
			_sampleRate = sampleRate;
		}

		public void SetFrequency(double value) {
			_currentFrequency = value;
		}

		public double GetNext(int sampleNumberInSecond) {
			double samplesPerOccilation = (_sampleRate / _currentFrequency);
			double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;
			double sinPart = Math.Sin(depthIntoOccilations * _radiansPerCircle);
			sinPart += (depthIntoOccilations - 0.5);
			return sinPart;
		}


		public void SetDetune(double detune) {
			_detune = detune;
		}

	}

	public class RoyalSawToothOccilator : SignWaveTest.IOccilator {

		private double _radiansPerCircle = Math.PI * 2;
		private double _currentFrequency = 2000;
		private double _sampleRate = 44100;

		private double _lastValue = 0.01;
		private double _maximumRateOfChange = 0.5;
		
	

		public RoyalSawToothOccilator(double sampleRate) {
			_sampleRate = sampleRate;
		}

		public void SetFrequency(double value) {
			_currentFrequency = value;
		}

		public double GetNext(int sampleNumberInSecond) {
			double samplesPerOccilation = (_sampleRate / _currentFrequency);
			double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;
		
			double sinPart = Math.Sin(depthIntoOccilations * _radiansPerCircle);	
			sinPart += (depthIntoOccilations - 0.5);

	
			if ((sinPart - _lastValue) > _maximumRateOfChange) {
				sinPart = _maximumRateOfChange;
			}
			if ((sinPart - _lastValue) < (_maximumRateOfChange * -1)) {
				sinPart = (_maximumRateOfChange * -1);
			}


			return sinPart;
		}




	}


	public class SawToothOccilatorDetunable : SignWaveTest.IOccilator, IDetuneable {

		private double _radiansPerCircle = Math.PI * 2;
		private double _currentFrequency = 2000;
		private double _sampleRate = 44100;
		private double _detune = 0.0;
		Random detuneRandom = new Random();
		public SawToothOccilatorDetunable(double sampleRate) {
			_sampleRate = sampleRate;
		}

		public void SetFrequency(double value) {
			_currentFrequency = value;
		}

		public double GetNext(int sampleNumberInSecond) {
			double samplesPerOccilation = (_sampleRate / _currentFrequency);

			if (_detune != 0.0) {
				samplesPerOccilation = samplesPerOccilation * (1 + (detuneRandom.NextDouble() * _detune));
			}

			double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;
			double sinPart = Math.Sin(depthIntoOccilations * _radiansPerCircle);
			sinPart += (depthIntoOccilations - 0.5);
			return sinPart;
		}

		#region IDetuneable Members

		public void SetDetune(double detune) {
			_detune = detune;
		}

		#endregion
	}


	public class SawToothOccilatorSteadyDetunable : SignWaveTest.IOccilator, IDetuneable {

		private double _radiansPerCircle = Math.PI * 2;
		private double _currentFrequency = 2000;
		private double _sampleRate = 44100;
		private double _maxDetune = 0.0;
		private double _currentDetune = 0.0;
		private bool _detuneHigh = true;
		private double chanceOfChanging = 0.1;
		private double maximumChangeInDetunePerChange = 0.05;


		Random detuneRandom = new Random();
		public SawToothOccilatorSteadyDetunable(double sampleRate) {
			_sampleRate = sampleRate;
		}

		public void SetFrequency(double value) {
			_currentFrequency = value;
		}

		public double GetNext(int sampleNumberInSecond) {
			double samplesPerOccilation = (_sampleRate / _currentFrequency);

			if (_maxDetune != 0.0) {
				
				if ((1 / chanceOfChanging) == (1/ detuneRandom.NextDouble()))
				{
					if (_detuneHigh){
						_currentDetune += (_maxDetune * maximumChangeInDetunePerChange);
					}else{
						_currentDetune -= (_maxDetune * maximumChangeInDetunePerChange);
					}
					if (_currentDetune >= _maxDetune || _currentDetune <= (_maxDetune * -1)){
						_detuneHigh = !_detuneHigh;
					}
				}
				
				samplesPerOccilation = samplesPerOccilation * ((1 - (_maxDetune/2)) + (_currentDetune));
			}

			double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;
			double sinPart = Math.Sin(depthIntoOccilations * _radiansPerCircle);
			sinPart += (depthIntoOccilations - 0.5);
			return sinPart;
		}

		public void SetDetune(double maxDetune) {
			_maxDetune = maxDetune;
		}

	}


}
