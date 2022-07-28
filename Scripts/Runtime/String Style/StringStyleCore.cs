﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.Internal {
/// <summary>
/// This class is a style, that <b>contains modifiers</b> (Color/Gradient/Size/etc.) for text.
/// <p/>
/// In other words, this class allows you to change style of your text.
/// <br/> So, for example, you can make <b>Bold Green</b> text.
/// </summary>
[Serializable]
public class StringStyleCore {

	public virtual bool IsEmpty => IsScopeEmpty || IsEmpty_WithoutScope;
	protected bool IsEmpty_WithoutScope =>
		!HasColor &&
		!HasGradient &&
		!HasSize &&
		!HasFont;

	internal bool IsScopeEmpty      => _scope.IsEmpty;
	internal bool HasNoColor        => !HasColor && !HasGradient;
	internal bool HasOnlyOneColor   => _color.HasColor() && !HasGradient;
	internal bool HasColor          => _color.HasColor();
	internal bool HasGradient       => _collectedColors != null || _cachedGradient != null;
	internal bool HasCachedGradient => _cachedGradient != null;
	internal bool HasSize           => _size.HasValue;
	internal bool HasFont      => fontStyle != UnityBuildInFontStyleType.None;

	internal StringStyleColor       Color           => _color;
	internal List<StringStyleColor> CollectedColors => _collectedColors;
	internal int                    SizeValue       => _size.Value;
	internal UnityBuildInFontStyleType FontStyle       => fontStyle;
	internal char[]                 Separators      => _separators;
	internal StringStyleScope       Scope           => _scope;
	internal Gradient Gradient => (_collectedColors != null && _cachedGradient == null)
		? _cachedGradient = GradientBuilder.CreateGradient(_collectedColors)
		: _cachedGradient;

	private bool StartedCollectingColorsForGradient => _collectedColors != null;

	[SerializeField] private StringStyleColor       _color;
	[SerializeField] private StringStyleSize        _size;
	[SerializeField] private UnityBuildInFontStyleType fontStyle;
	[SerializeField] private StringStyleScope       _scope;
	[SerializeField] private char[]                 _separators;

	[SerializeField] [CanBeNull] private Gradient _cachedGradient;
	/// <remarks> Count can only be between 2 and 8 including. 8 is Unity's Gradient max count of colorKeys </remarks>
	[SerializeField] [CanBeNull] private List<StringStyleColor> _collectedColors;

	private Color3Set.Modifier _currentColorModifier;

	internal virtual void PrepareColor(ColorType gffColor) => __PrepareColor(gffColor, null);
	internal virtual void PrepareColor(string strColor)    => __PrepareColor(ColorType.Undefined, strColor);
	internal virtual void __PrepareColor(ColorType gffColor, string strColor) {
		if (!StartedCollectingColorsForGradient && !HasColor)
			_color.__SetColor(gffColor, strColor, GetColorModificator());
		else
			PrepareColorAdditionally(gffColor, strColor);
	}

	internal virtual void PrepareColorAdditionally(ColorType gffColor, string strColor) {
		_collectedColors ??= new List<StringStyleColor> {_color};
		if (_collectedColors.Count >= 8)
			return;
		_collectedColors.Add(new StringStyleColor().__SetColor(gffColor, strColor, GetColorModificator()));
	}

	internal virtual void PrepareGradient(Gradient gradient) { if (gradient != null) _cachedGradient = gradient; }
	internal virtual void PrepareColorModification(Color3Set.Modifier modifier) => _currentColorModifier = modifier;
	internal virtual void PrepareSize(int size) => _size.Value = size;
	internal virtual void PrepareFontStyle(UnityBuildInFontStyleType fontStyle) => this.fontStyle =
		(this.fontStyle == UnityBuildInFontStyleType.Bold && fontStyle == UnityBuildInFontStyleType.Italic) ||
		(this.fontStyle == UnityBuildInFontStyleType.Italic && fontStyle == UnityBuildInFontStyleType.Bold)
			? UnityBuildInFontStyleType.BoldItalic : fontStyle;

	internal virtual void PrepareScope(Range range)                                                      => PrepareScope(range.Start.Value, range.Start.IsFromEnd, range.End.Value, range.End.IsFromEnd);
	internal virtual void PrepareScope(Index index)                                                      => PrepareScope(index.Value, index.IsFromEnd);
	internal virtual void PrepareScope(int   index)                                                      => _scope.ApplyScope(index);
	internal virtual void PrepareScope(int   index,      bool isFromEnd)                                 => _scope.ApplyScope(index, isFromEnd);
	internal virtual void PrepareScope(int   startIndex, int  endIndex)                                  => _scope.ApplyScope(startIndex, endIndex);
	internal virtual void PrepareScope(int   startIndex, bool isFromEnd1, int endIndex, bool isFromEnd2) => _scope.ApplyScope(startIndex, isFromEnd1, endIndex, isFromEnd2);
	internal virtual void PrepareScope(float percentage)                                                 => _scope.ApplyScope(percentage);

	private Color3Set.Modifier GetColorModificator() {
		var usedModificator = _currentColorModifier;
		_currentColorModifier = Color3Set.Modifier.None;
		return usedModificator;
	}
}
}
