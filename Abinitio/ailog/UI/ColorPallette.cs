// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.ColorPallette
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.Drawing;

namespace ADTechnology.AbInitio.UI
{
  internal class ColorPallette
  {
    private Color[] col = new Color[8]
    {
      Color.Black,
      Color.Blue,
      Color.Red,
      Color.Purple,
      Color.Brown,
      Color.DarkCyan,
      Color.DarkGreen,
      Color.DarkOrange
    };
    private int ix = 0;

    public Color Color
    {
      get
      {
        return this.col[this.ix];
      }
    }

    public Color GetNextColor()
    {
      Color color = this.col[this.ix];
      ++this.ix;
      if (this.ix >= this.col.Length)
        this.ix = 0;
      return color;
    }

    public Color GetColor(int index)
    {
      if (this.ix > -1 && this.ix < this.col.Length)
        this.ix = index;
      return this.col[this.ix];
    }
  }
}
