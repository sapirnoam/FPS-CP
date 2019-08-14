using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToSite : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("www.noam3d.com");
    }
    public void ApplyBeta()
    {
        Application.OpenURL("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=QRDLH4CRB38SU&source=url");
    }
    public void SpreadShirt()
    {
        Application.OpenURL("https://shop.spreadshirt.com/pofleMerch");
    }


}
