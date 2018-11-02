using System;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;

namespace Common.TrueDBGrid
{
    public abstract class TrueDBGridValidator : IBaseValidator
    {
        public C1TrueDBGrid grid;

        public virtual void Initialize(Control c)
        {
            this.grid = c as C1TrueDBGrid;
            Initialize();
        }

        public abstract void Initialize();
    }
}
