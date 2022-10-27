using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastBatteries : MonoBehaviour
{

    private int batteriesObtained = 0;
    private int currentBatteries = 1;
    public int batteryRecharge; //cuanto recarga cada pickup
    public Text count; //cuantos pickups de bateria conseguiste
    public FlashlightLife wasteBattery; //cantidad de vida de bateria

    public Inventory _inventory;


    void Update()
    {
        _inventory.GetComponent<Inventory>().InventoryOpen();
        print("raycast batteries: inventoryopen");


        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 20f))
        {
            if (hit.transform.tag == "batteries" && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
            {
                batteriesObtained += currentBatteries;
                count.text = batteriesObtained.ToString("f0") + "/5";

                wasteBattery.timer += batteryRecharge;

                GameObject itemPickedUp = hit.transform.gameObject;
                Items item = itemPickedUp.GetComponent<Items>();
                _inventory.AddItem(itemPickedUp, item.id, item.type, item.icon);

                CanvasManager.instance.TurnOnCanvas("CanvasBatteries");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 10f);
    }
}
