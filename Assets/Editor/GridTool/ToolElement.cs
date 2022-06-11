using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolElement : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ToolElement, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlColorAttributeDescription color = new UxmlColorAttributeDescription { name = "color-attr", defaultValue = Color.green };
        UxmlStringAttributeDescription gridObject = new UxmlStringAttributeDescription { name = "gridobject-attr", defaultValue = null};
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as ToolElement;

            ate.colorAttr = color.GetValueFromBag(bag, cc);
            ate.gridobjectAttr = gridObject.GetValueFromBag(bag, cc);

        }
    }

    public Color colorAttr { get; set; }
    public string gridobjectAttr { get; set; }
}
