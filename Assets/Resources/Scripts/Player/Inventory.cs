using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	[SerializeField]
	List<Attachable> inventoryList;

	[SerializeField]
	int position;

	int next { get { return mod(position + 1, SizeOfInventory); } }

	int previous { get { return mod(position - 1, SizeOfInventory); } }

	[SerializeField]
	int SizeOfInventory; /* GlobalStored */

	// Mutex for inventory switching
	bool mutex;

	MessageBox msgbox;

	WaitForSecondsIEnum wait;

	int mod(int a, int n)
	{
		int result = a % n;
		if ((result<0 && n>0) || (result>0 && n<0)) {
			result += n;
		}
		return result;
	}

	// Use this for initialization
	void Start () {
		SizeOfInventory = GlobalStore.now.SizeofInventoryWithoutBag;

		for (int i = 0; i < SizeOfInventory; i++) {
			inventoryList.Add (null);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Left item
		if (Input.GetAxis ("Horizontal") > 0) {
			Scroll (next);
		} 
		// Right item
		if (Input.GetAxis ("Horizontal") < 0) {
			Scroll (previous);
		}

		if (Input.GetButtonDown ("Fire2") && inventoryList [position] != null) {
			inventoryList [position].Unattach ();
			inventoryList [position] = null;
		}

		// Show current item as UI
		if (!MessageBox.HasMessageBoxOnScreen && msgbox != null) {
			msgbox.Show ();
			msgbox = null;
		}
	}

	void Scroll (int nextIndex) {
		if (!mutex) {
			if (inventoryList [position] != null)
				inventoryList [position].Pause ();
			if (inventoryList [nextIndex] != null)
				inventoryList [nextIndex].Resume ();
			position = nextIndex;

			msgbox = new MessageBox (
				this, 
				inventoryList [position] == null ? "[ Empty ]" : inventoryList [position].name, 
				MessageBox.DURATION_SHORT, 
				100
			).SetColor (new Color (0.5f, 0.5f, 0.5f));

			// Lock scrolling for 0.2 seconds
			mutex = true;
			new WaitForSecondsIEnum (0.2f, delegate(object[] objects) {
				mutex = false;
			}).Run (this);
		}
	}

	public void Enlarge () {
		// If bag is found
		for (int i = SizeOfInventory - 1; i < GlobalStore.now.SizeofInventoryWithBag; i++)
			inventoryList.Add (null);
		
		SizeOfInventory = GlobalStore.now.SizeofInventoryWithBag;
	}

	public void Add (Attachable item) {
		if (inventoryList [position] == null)
			item.Attach ();
		inventoryList [position] = item;
	}
}
