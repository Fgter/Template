using QFramework;
using Define;
using Models;
using UnityEngine;

class GetIconItemDefineQuery : AbstractQuery<IIconItemDefine>
{
    int id;
    public GetIconItemDefineQuery(int id)
    {
        this.id = id;
    }
    protected override IIconItemDefine OnDo()
    {
        DefineModel model = this.GetModel<DefineModel>();
        if (model.iconItemDefines.ContainsKey(id))
        {
            return model.iconItemDefines[id];
        }
        Debug.LogError(string.Format("DefineModel中IconItemDefines中找不到id为{0}的item"));
        return default;
    }
}
