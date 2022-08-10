using System;
using UnityEngine;

namespace Graffiti.Internal {
[Serializable]
internal struct StringStyleScope {

	internal bool IsEmpty => !_useIndex && ! _useRange && !_usePercentage;

	[SerializeField] private bool  _useIndex;
	[SerializeField] private bool  _useRange;
	[SerializeField] private bool  _usePercentage;

	[SerializeField] private int   _from;
	[SerializeField] private int   _to;
	[SerializeField] private bool  _isFromEnd1;
	[SerializeField] private bool  _isFromEnd2;
	[SerializeField] private float _percentage;


	internal (int fromN, int toN) CreateScope(int length) {

		if (IsEmpty)
			return (0, 0);

		int lastIndex = length - 1;

		if (_useRange) {
			_from = Mathf.Clamp(_from, 0, lastIndex);
			_to   = Mathf.Clamp(_to, 0, lastIndex);

			if (_isFromEnd1)
				_from = lastIndex - _from;
			if (_isFromEnd2)
				_to = lastIndex - _to;

			if (_from > _to)
				return (_to, _from);
			return (_from, _to);
		}

		if (_useIndex) {
			_from = Mathf.Clamp(_from, 0, lastIndex);

			if (_isFromEnd1)
				_from = lastIndex - _from;

			return (_from, _from);
		}

		if (_usePercentage) {
			bool isFromEnd = _percentage < 0;
			if (isFromEnd)
				_percentage *= -1;
			_percentage = Mathf.Clamp01(_percentage);

			if (_percentage > .997f) // note: to prevent float from being compared to 1
				return (0, lastIndex);

			if (isFromEnd)
				return (lastIndex - (int) (lastIndex*_percentage), lastIndex);
			return (0, (int) (lastIndex*_percentage));
		}

		return (0, 0);
	}


	internal void ApplyScope(int startIndex, bool isFromEnd1, int endIndex, bool isFromEnd2) {
		_useRange = true;
		_from  = startIndex;
		_to = endIndex;
		_isFromEnd1 = isFromEnd1;
		_isFromEnd2 = isFromEnd2;
	}

	internal void ApplyScope(int startIndex, int endIndex) {
		_useRange = true;
		_from  = startIndex;
		_to = endIndex;
	}

	internal void ApplyScope(int index, bool isFromEnd) {
		_useIndex = true;
		_from = index;
		_isFromEnd1 = isFromEnd;
	}

	internal void ApplyScope(float percentage) {
		if (Mathf.Abs(percentage) < .003f) // note: to prevent float from being compared to 0
			return;
		_usePercentage = true;
		_percentage = percentage;
	}
}
}
