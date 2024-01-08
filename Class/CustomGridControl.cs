using System;using QLBV_Database;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Registrator;
using System.Globalization;

namespace QLBV.Class
{
    public class CustomGridView : GridView
    {
        public CustomGridView() : base()
        {
            this.CustomUnboundColumnData += MyGridView_CustomUnboundColumnData;
        }

        void MyGridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName.Contains("Unb"))
            {
                string field = e.Column.FieldName.Substring(0, e.Column.FieldName.IndexOf("Unb"));
                string val = (e.Row as DataRowView)[field].ToString();
                string processedString = CustomGridView.RemoveDiacritics(val, true);
                e.Value = val + processedString;
            }
        }

        protected internal virtual void SetGridControlAccessMetod(GridControl newControl)
        {
            SetGridControl(newControl);
        }

        protected override string OnCreateLookupDisplayFilter(string text, string displayMember)
        {
            List<CriteriaOperator> subStringOperators = new List<CriteriaOperator>();
            foreach (string sString in text.Split(' '))
            {
                string exp = LikeData.CreateContainsPattern(sString);
                List<CriteriaOperator> columnsOperators = new List<CriteriaOperator>();
                foreach (GridColumn col in Columns)
                {
                    if (col.Visible && col.ColumnType == typeof(string))
                        columnsOperators.Add(new BinaryOperator(col.FieldName, exp, BinaryOperatorType.Like));
                }
                subStringOperators.Add(new GroupOperator(GroupOperatorType.Or, columnsOperators));
            }
            return new GroupOperator(GroupOperatorType.And, subStringOperators).ToString();
        }

        protected override void RefreshVisibleColumnsList()
        {
            base.RefreshVisibleColumnsList();
            // add required unbound columns
            foreach (GridColumn column in VisibleColumns)
            {
                string name = column.FieldName + "Unb";
                GridColumn col = Columns.OfType<GridColumn>().Where(c => c.FieldName == name).FirstOrDefault();
                if (col != null) continue;
                GridColumn unboundCol = Columns.AddField(column.FieldName + "Unb");
                unboundCol.UnboundType = DevExpress.Data.UnboundColumnType.String;
                column.FieldNameSortGroup = unboundCol.FieldName;
                column.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.True;
            }
        }

        protected override ColumnFilterInfo CreateFilterRowInfo(GridColumn column, object _value)
        {
            string strVal = _value == null ? null : _value.ToString();
            if (_value == null || strVal == string.Empty) return ColumnFilterInfo.Empty;
            strVal = CustomGridView.RemoveDiacritics(strVal, true);
            AutoFilterCondition condition = ResolveAutoFilterCondition(column);
            CriteriaOperator op = CreateAutoFilterCriterion(column, condition, _value, strVal);
            return new ColumnFilterInfo(ColumnFilterType.AutoFilter, _value, op, string.Empty);
        }

        public static IEnumerable<char> RemoveDiacriticsEnum(string src, bool compatNorm, Func<char, char> customFolding)
        {
            foreach (char c in src.Normalize(compatNorm ? NormalizationForm.FormKD : NormalizationForm.FormD))
                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    case UnicodeCategory.NonSpacingMark:
                    case UnicodeCategory.SpacingCombiningMark:
                    case UnicodeCategory.EnclosingMark:
                        //do nothing
                        break;
                    default:
                        yield return customFolding(c);
                        break;
                }
        }

        public static IEnumerable<char> RemoveDiacriticsEnum(string src, bool compatNorm)
        {
            return RemoveDiacritics(src, compatNorm, c => c);
        }

        public static string RemoveDiacritics(string src, bool compatNorm, Func<char, char> customFolding)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in RemoveDiacriticsEnum(src, compatNorm, customFolding))
                sb.Append(c);
            return sb.ToString();
        }

        public static string RemoveDiacritics(string src, bool compatNorm)
        {
            return RemoveDiacritics(src, compatNorm, c => c);
        }

        protected override string ViewName { get { return "CustomGridView"; } }
        protected virtual internal string GetExtraFilterText { get { return ExtraFilterText; } }
    }

    public class CustomGridControl : GridControl
    {
        public CustomGridControl() : base() { }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CustomGridInfoRegistrator());
        }

        protected override BaseView CreateDefaultView()
        {
            return CreateView("CustomGridView");
        }

    }

    public class CustomGridPainter : GridPainter
    {
        public CustomGridPainter(GridView view) : base(view) { }

        public virtual new CustomGridView View { get { return (CustomGridView)base.View; } }

        protected override void DrawRowCell(GridViewDrawArgs e, GridCellInfo cell)
        {
            cell.ViewInfo.MatchedStringUseContains = true;
            cell.ViewInfo.MatchedString = View.GetExtraFilterText;
            cell.State = GridRowCellState.Dirty;
            e.ViewInfo.UpdateCellAppearance(cell);
            base.DrawRowCell(e, cell);
        }
    }

    public class CustomGridInfoRegistrator : GridInfoRegistrator
    {
        public CustomGridInfoRegistrator() : base() { }
        public override BaseViewPainter CreatePainter(BaseView view) { return new CustomGridPainter(view as DevExpress.XtraGrid.Views.Grid.GridView); }
        public override string ViewName { get { return "CustomGridView"; } }
        public override BaseView CreateView(GridControl grid)
        {
            CustomGridView view = new CustomGridView();
            view.SetGridControlAccessMetod(grid);
            return view;
        }

    }

}
