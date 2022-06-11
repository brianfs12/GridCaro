using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CellElement : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CellElement, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlColorAttributeDescription color = new UxmlColorAttributeDescription { name = "color-attr", defaultValue = Color.green};
        UxmlIntAttributeDescription posx = new UxmlIntAttributeDescription { name = "posx-attr", defaultValue = 0 };
        UxmlIntAttributeDescription posy = new UxmlIntAttributeDescription { name = "posy-attr", defaultValue = 0 };
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as CellElement;

            ate.colorAttr = color.GetValueFromBag(bag, cc);
            ate.posxAttr = posx.GetValueFromBag(bag, cc);
            ate.posyAttr = posy.GetValueFromBag(bag, cc);
        }
    }

    public Color colorAttr { get; set; }
    public int posxAttr { get; set; }
    public int posyAttr { get; set; }

    }
