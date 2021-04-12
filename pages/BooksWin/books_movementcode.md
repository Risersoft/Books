---
title: Movement Code
keywords: Movement Code
sidebar: bookswin_sidebar
permalink: books-nirvana-win/movement-code.html
folder: BooksWin
hide_sidebar: false
comments: false
---

# Movement Code

## Create

**Step-1**: Click on **New -> Configuration-> Movement Code**. Movement Code window will appear.

![](/images/MovementCodeCreateMenu.png)

**Step-2**: Fill the details.

![](/images/MovementCodeForm.png)

**Step-3**:  Click on **Save** button. 

## List

Click on **Configuration-> Movement Codes**. **Movement Codes** list will appear.

![](/images/MovementCodeList.png)

### Movement Code List


**Goods Reciept**

|Code| Description|	Reference Document| Sp. Stock|	Sp. Stock2|	Stock Stage| Stock Stage2|	Qty Type Source| Qty Type Destination|	BOM Code|	Doc. No.|	OD Note|
---|---|---|---|---|---|---|---|---|---|---|---|
101|GR against Purchase Order| PO, LPO| N| | CG,CT, RM,ST| | | UR ,QI, R| | Y| |
101|GR against Purchase Order| PO, LPO| C| | CG,CT, RM,ST| | | UR| | Y| |
101|GR against Purchase Order| PO, LPO| V| | CG,CT, RM,ST| | | UR| | Y| |
101|GR against Purchase Order| PO, LPO| Z| | CG,CT, RM,ST| | | UR| | Y| |
103|GR to Transit against Purchase Order| PO, LPO| N| | CG,CT, RM,ST| | | TR| | Y| |
111|GR against JWO| JWO| N| | RM, WIP, FG| | | UR, QI, R| 241| Y| |
112|GR by-product against JWO|	|	N|	| RM|	|	| UR| 241|	Y| |
121|GR against MO|MO|	N|	|WIP, FG|	|	|UR, QI, R|	231, 232| | |
122|GR by-product against MO|	|N| |	RM|	|	|	UR|	231, 232|
131|GR without Purchase Order from vendor|	|	N| |RM, ST,CG, CT|	| |	UR, R|	|Y
131|GR without Purchase Order from vendor| |	K|		|RM,ST, CG,CT|		|  |	UR, R|	|	Y| |
131|GR without Purchase Order from vendor|	|Z|		|RM,ST, CG, CT| | |	UR, R|	|	Y	|
132|GR to Transit Without Purchase Order from vendor|	|	N	|	|RM,ST, CG, CT|	|	|	TR|	|	Y
141|GR Without MO from shop floor| | Z| | WIP,FG| | |UR,R| | | |
141|GR Without MO from shop floor| | N| | WIP,FG| | |UR,R|231, 232 | | |
151|GR Scrap without MO from shop floor| | N| | RM,FG| | |UR|231, 232 | | |
152|GR Scrap without JWO from Vendor| | N| | RM| | |UR|231, 232 | | |
161|GR Consignment Stock from Customer| | J| |FG, WIP, RM| | |UR||Y| |
162|GR against Customer Repair Order| | J| | RM,WIP,FG| | |UR| | Y| |
163|Returns from Customer| | N| | RM,FG| | |UR, QI, R| |Y | |
164|GR Valuated Stock from Customer| | N| | RM,FG| | |UR| |Y | |
501|Initial entry of stock| | N| |RM, CG,CT,WIP, FG,ST| | |UR, QI, R| | | |
501|Initial entry of stock| | J| | RM, CG,CT,WIP, FG,ST| | |UR, QI, R| | | |
501|Initial entry of stock| | K| | RM, CG,CT,WIP, FG,ST| | |UR, QI, R| | | |
501|Initial entry of stock| | V| | RM, CG,CT,WIP, FG,ST| | |UR | | |
501|Initial entry of stock| | Z| |RM, CG,CT,WIP, FG,ST| | |UR, QI, R| | | |

**Goods Issues**

|Code| Description|	Reference Document| Sp. Stock|	Sp. Stock2|	Stock Stage| Stock Stage2|	Qty Type Source| Qty Type Destination|	BOM Code|	Doc. No.|	OD Note|
---|---|---|---|---|---|---|---|---|---|---|---|
201|GI to Consumption| |Z|  | RM , CG,CT| |UR| | |Y| |
201|GI to Consumption| |N|  | RM , CG,CT| |UR| | |Y| |
201|GI to Consumption| |K|  | RM , CG,CT| |UR| | |Y| |
201|GI to Consumption| |C|  | RM , CG,CT| |UR| | |Y| |
201|GI to Consumption| |V|  | RM , CG,CT| |UR| | |Y| |
211|GI to Asset Maintenance| |N|  | RM , CG, CT| |UR| | |Y| |
221|GI to Asset Augmentation| |N|  | RM , CG,CT,WIP, FG| |UR| | |Y| |
231|GI against MO -Stock at Floor| |N|  |  RM, WIP| |UR| | || |
231|GI against MO -Stock at Floor| |Z|  | RM, WIP,| |UR| | | | |
232|GI against MO -Stock in store| |Z|  | RM, WIP, FG| |UR| | | | |
232|GI against MO -Stock in store| |N|  | RM, WIP, FG| |UR| | | | |
241|GI against JWO| |V|  | RM , CG,CT,WIP, FG,ST| |UR, R| | |Y | |
251|	GI for Sale| | N| |ST,FG|	|UR	|	| |Y|Y|
251|	GI for Sale| | V| |ST,FG|	|UR	|	| |Y|Y|
252|	GI from transit for Sale against Sales order|	|N|		|ST, FG, RM|	|	TR	|	|	| Y|	Y
261|	GI for Extra Items|		|N		| |RM|		|UR| | |			Y|	Y
271	|GI for return Delivery to Vendor|	|Z|	|	CG, CT, RM, ST| |UR, R|	| |Y|	Y
271|GI for return Delivery to Vendor|	|	N|	|	CG, CT, RM, ST|	|	UR, R|	|	|	Y|	Y
271|GI for return Delivery to Vendor|	|	K|	|	CG, CT, RM, ST|	|	UR, R|	|	|	Y|Y
272|GI for return delivery to customer|	|	C|	|	RM, CG, CT, WIP, FG, ST|	|	UR|	|	|	Y|
273|GI for customer consignment return delivery|	|	J|	|	RM, CG, CT, WIP, FG, ST|	|	UR|	|	|	Y|	Y
281|GI to sampling / scrap|	|	N|	|	RM, ST, CG, CT|	|	UR, QI, R|	|	|	Y|
281|GI to sampling / scrap|	|	K|	|	RM, ST, CG, CT|	|	UR, QI, R|	|	|	Y	|
281|GI to sampling / scrap|	|	Z|	|	RM, ST, CG, CT|	|	UR, QI, R|	|	|	Y|

**Transfer Posting**

|Code| Description|	Reference Document| Sp. Stock|	Sp. Stock2|	Stock Stage| Stock Stage2|	Qty Type Source| Qty Type Destination|	BOM Code|	Doc. No.|	OD Note|
---|---|---|---|---|---|---|---|---|---|---|---|
301|Transfer within a plant – remove from storage|	|	Z|	|	RM, CG, CT, WIP, FG, ST|	|	UR|TR
301|Transfer within a plant – remove from storage|	|	N|	|	RM, CG, CT, WIP, FG, ST|	|	UR|TR|
232|GI against MO - Stock in Store|	|	Z|	|	RM, WIP, FG|	|	UR
232|GI against MO - Stock in Store|	|	N|	|	RM, WIP, FG|	|	UR
241|GI against JWO|	|	V|	|	RM, CG, CT, WIP, FG, ST|	|	UR, R|	|	|	Y
251|GI for Sale|	|	N|	|	ST, FG|	|	UR|	|	|	Y|	Y
251|GI for Sale| |	V|	|	ST, FG|	|	UR|	|	|	Y|	Y
252|GI from transit for Sale against Sales order|	|	N| |	ST, FG, RM|	|	TR|	|	|	Y|	Y
261|GI for Extra Items|	|	N|	|RM|	|	UR|	|	|	Y|	Y
271|GI for return Delivery to Vendor|	|	Z|	|CG, CT, RM, ST|	|	UR, R|	|	|	Y|	Y
271|GI for return Delivery to Vendor|	|	N|	|	CG, CT, RM, ST|	|UR, R|	|	|	Y|	Y
271|GI for return Delivery to Vendor|	|	K|	|CG, CT, RM, ST|	|	UR, R|	|	|	Y|	Y
272|GI for return delivery to customer|	|	C|	|	RM, CG, CT, WIP, FG, ST|	|	UR|	|	|	Y
273|GI for customer consignment return delivery|	|	J|	|	RM, CG, CT, WIP, FG, ST|	|	UR|	|	|	Y|	Y
281|GI to sampling / scrap|	|	N|	|	RM, ST, CG, CT|	|	UR, QI, R|	|	|	Y
281|GI to sampling / scrap|	|	K|	|	RM, ST, CG, CT|	|	UR, QI, R|	|	|	Y
281|GI to sampling / scrap|	|	Z|	|	RM, ST, CG, CT|	|	UR, QI, R|	|	|	Y

**Transfer Posting**

|Code| Description|	Reference Document| Sp. Stock|	Sp. Stock2|	Stock Stage| Stock Stage2|	Qty Type Source| Qty Type Destination|	BOM Code|	Doc. No.|	OD Note|
---|---|---|---|---|---|---|---|---|---|---|---|
301|Transfer within a plant – remove from storage|	|	Z|	|	RM, CG, CT, WIP, FG, ST|	|	UR|	TR|
301|Transfer within a plant – remove from storage|	|	N|	|	RM, CG, CT, WIP, FG, ST|	|UR|	TR
315|Transfer Zero Valued Stock plant to plant – remove from storage|	|	Z|	|	RM, ST, CG, CT|	|	UR|	TR	|	|Y	|Y
321|Transfer from UR within a storage location|	|	Z|	|	RM, CG, CT, WIP, FG, ST|	|	UR|	QI, R
321|Transfer from UR within a storage location|	|	N|	|	RM, CG, CT, WIP, FG, ST|	|	UR|	QI, R
321|Transfer from UR within a storage location|	|	J|	|	RM, CG, CT, WIP, FG, ST|	|	UR|	QI, R
321|Transfer from UR within a storage location|	|	K|	|RM, CG, CT, WIP, FG, ST|	|	UR|	QI, R
322|Transfer out of QI within a storage location|	|	N|	|	RM, CG, CT, WIP, FG, ST|	|	QI|	UR, R
322|Transfer out of QI within a storage location|	|	J|	|	RM, CG, CT, WIP, FG, ST|	|	QI|	UR, R
322|Transfer out of QI within a storage location|	|	K|	|	RM, CG, CT, WIP, FG, ST|	|	QI|UR, R
322|Transfer out of QI within a storage location|	|	Z|	|	RM, CG, CT, WIP, FG, ST|	|	QI|	UR, R|
323|Transfer from R within a storage location| |	Z|	|	RM, CG, CT, WIP, FG, ST|	|	R	|UR, QI
323|Transfer from R within a storage location| |	N|	|	RM, CG, CT, WIP, FG, ST|	|	R	|UR, QI
323|Transfer from R within a storage location| |	J|	|	RM, CG, CT, WIP, FG, ST|	|	R|	UR, QI
323|Transfer from R within a storage location| |	K	|	|RM, CG, CT, WIP, FG, ST|	|	R	|UR, QI
323|Transfer from R within a storage location| |		K|	|	RM, CG, CT, WIP, FG, ST|	|	R	|UR, QI
411|Transfer from consignment to own stock|	|	K|	N|	RM, CG, CT, WIP, FG, ST|	|	UR, QI, R|	UR, QI, R
421|Transfer from own stock to SC vendor JWO| |	N|V|	RM, CG, CT, WIP, FG, ST|	|	UR, R|	UR, R|	|	Y	|Y
422|Transfer from store to floor|	|	N|	|	RM, CG, CT, WIP, FG, ST|	|	UR|	UR
423|Transfer Rejections from floor to store|	|	N|	|	RM, CG, CT, WIP, FG, ST|	|	UR, R|	R
424|Transfer leftover from floor to store | |N |	|RM, CG, CT, WIP, FG, ST|	|	UR|	UR
425|Transfer from Vendor to Store|	|	N| V	|RM, CG, CT, WIP, FG, ST|	|	UR, R|	UR, R
426|Transfer Repair Finish Good from store to floor|	|	N|	|	FG|	|	UR|	UR	|	|	|Y
427	|Transfer from Customer to Store|	|	N|	C|	RM, CG, CT, WIP, FG, ST|	|	UR|	UR
431|Transfer from own stock to Customer|	|	N|	C|	RM, CG, CT, WIP, FG, ST|	|	UR| UR|	|	Y
441	|Transfer from work order A to work order B for ETO Item|		|N	|	|RM, CG, CT, WIP, FG, ST|	|	UR| UR
502	|RM Excisable to ST|	|	N|	|	RM|	ST|	UR|	UR|	|	|	Y
503|	RM Non Excisable to ST|	|	N|	|	RM|	ST|	UR|	UR|	|	|

**Reference Table**

|Reference Document | Special Stock  |Stock Stage  | QTY Type Source/Destination |
|----|----|----|----|
|PO- Purchase Order| N- Normal |	RM- Raw Material |	UR- Unrestricted|
|LPO- Local Purchase Order|	K- Vendor Consignment|	CG- Consumable Goods|	TR-Transit|
|MO-Manufacturing Order|	V-Own Stock to Vendor|	CT- Consumable Tools| 	R-Restricted/Rejected|
|JWO- Job Work Order|	C- Own Stock to Customer|	FG- Finished Goods|	QI-Under Quality Inspection|
|OTH- Other|	J- Customer Consignment|	WIP- Work in Progress	|
|MI- Material Indent|	|	ST-Stock in Trade|
|OD Note- Outgoing Delivery Note|
|MV- Material Voucher|


## Edit

**Step-1**: Select the movement code from movement code list that you want to edit. **Right click** and select **Edit Movement Code** option. ***Movement Code** form will appear.

![](/images/MovementCodeEdit.png)

**Step-2**: Edit the details and click on **Save** button.