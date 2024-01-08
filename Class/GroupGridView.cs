using System;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.Data;
using DevExpress.XtraGrid.Misc;
namespace DevExpress.XtraGrid
{
    public class MyGridView : GridView
    {
        public MyGridView(GridControl grid) : base(grid)
        {
            this.OptionsBehavior.AutoExpandAllGroups = true;
            this.OptionsView.ShowGroupedColumns = true;
            this.OptionsView.GroupFooterShowMode = GroupFooterShowMode.Hidden;
        }
        public MyGridView() : this(null) { }
        protected override string ViewName { get { return "MyGridView"; } }
        protected override BaseGridController CreateDataController() { return new MyDataController(); }
    }
    public class MyGridControl : GridControl
    {
        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            collection.Add(new MyGridViewInfoRegistrator());
        }
    }
}
namespace DevExpress.XtraGrid.Misc
{
    public class MyGridViewInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName { get { return "MyGridView"; } }
        public override BaseView CreateView(GridControl grid) { return new MyGridView(grid as GridControl); }
        public override BaseViewInfo CreateViewInfo(BaseView view) { return new MyGridViewInfo(view as MyGridView); }
    }
    public class MyDataController : CurrencyDataController
    {
        protected override void BuildVisibleIndexes()
        {
            base.BuildVisibleIndexes();
            if (GroupedColumnCount == 0) return;
            int[] indexes = new int[VisibleIndexes.Count];
            VisibleIndexes.CopyTo(indexes, 0);
            VisibleIndexes.Clear();
            foreach (int rowHandle in indexes)
            {
                if (IsGroupRowHandle(rowHandle) && QLBV.DungChung.Bien.MaBV == "14017")
                {
                    if (GetParentGroupRow(rowHandle) != null)
                    {
                        if ((GetGroupRowValue(GetParentGroupRow(rowHandle)) != null))
                        {
                            var rowParent = GetRow(GetParentRowHandle(rowHandle));
                            if (rowParent != null && GetValObjDy(rowParent, "IDNhom") != null && GetValObjDy(rowParent, "IDNhom").ToString() != "4" && GetValObjDy(rowParent, "IDNhom").ToString() != "5" && GetValObjDy(rowParent, "IDNhom").ToString() != "6")
                            {
                                continue;
                            }
                        }
                    }
                }
                VisibleIndexes.Add(rowHandle);
            }
        }
        public object GetValObjDy(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }
    }
    public class MyGridViewInfo : GridViewInfo
    {
        public MyGridViewInfo(GridView view) : base(view) { }
        public override int GetRowFooterCount(int rowHandle, int nextRowHandle, bool isExpanded)
        {
            GroupRowInfo rowInfo = View.DataController.GroupInfo.GetGroupRowInfoByControllerRowHandle(rowHandle);
            GroupRowInfo newRowInfo = View.DataController.GroupInfo.GetGroupRowInfoByControllerRowHandle(nextRowHandle);
            newRowInfo = GetStartGroup(newRowInfo, nextRowHandle);
            if (newRowInfo == null || newRowInfo == rowInfo) return base.GetRowFooterCount(rowHandle, nextRowHandle, isExpanded);
            return base.GetRowFooterCount(rowHandle, newRowInfo.Handle, isExpanded);
        }
        GroupRowInfo GetStartGroup(GroupRowInfo newRowInfo, int nextRowHandle)
        {
            if (newRowInfo == null) return null;
            while (newRowInfo.ParentGroup != null)
            {
                if (newRowInfo.ParentGroup.ChildControllerRow == nextRowHandle)
                    newRowInfo = newRowInfo.ParentGroup;
                else
                    break;
            }
            return newRowInfo;
        }
    }
}