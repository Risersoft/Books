---
title: Material Voucher
keywords: Material Voucher
sidebar: bookswin_sidebar
permalink: books-nirvana-win/material-voucher-purchase.html
folder: BooksWin
hide_sidebar: false
comments: false
---

# Material Voucher

## Create

**STEP-1**: Click on **New ->Voucher->Material Voucher**. **Material Voucher** window will appear.

![](/images/matvoucher-create.png)

**STEP-2**: Fill the details and click on **Save**.

![](/images/matvoucher-form.png)

## List

Click on **Vouchers-> Material Voucher**. **Material Vouchers list** will appear.

![](/images/matvoucher-list.png)
 
Click on **Vouchers->Voucher->Transfer Material Vouchers**. Material Vouchers list will appear.

![](/images/matvoucher-transfer-list.png)

## Edit

**STEP-1**: Select the material voucher from list that you want to edit.

**STEP-2**: Right click on it and select **Edit Material Voucher** option.

![](/images/matvoucher-edit.png)

**STEP-3**: Edit details and click on **Save**.

### Material Voucher Against PO(GR)

**STEP-1**: Select **Voucher Type** (Goods Receipt), **Ref Document Type** (PO), **Tax Type** and **Department**.

**STEP-2**: Click on **Select Document**. Select **Purchase Order** window will appear. This window depends on the selection of Ref Document. Select purchase order from the list appear and click on **OK**. Click on **Execute Selection**.

![](/images/pogr-detail.png)

**STEP-3**: Select window will appear. Select the required row. Enter **Qty(Quantity)** if this field is blank otherwise it will generate error. Click on **OK**.

![](/images/pogr-detail-select.png)

Entries will fill automatically according to purchase order.

![](/images/pogr-detail-filled.png)

**STEP-4**: Click on **Pricing** tab in **Header** section and Select **Procedure** and **Slab**.

**STEP-5**: Click on **Document** tab in **Header** section and enter **Challan No.** and **GR No..**. Select **Challan Date, Challan Received** and **Transporter**.

![](/images/pogr-document.png)

**STEP-6**: Click on **Save** button.

![](/images/pogr-document-save.png)

**Note:** If you want to add some other items in Item List section against PO, you can do it manually.
1. Select **Vendor** and click on **Add New**. New row will appear in Item List.

![](/images/pogr-additem.png)

2. Click on **Material tab** in **Item Details** section. Select **Item Code**. You can select only that item which is available in any purchase order/local purchase order against selected vendor.

3. Click on **Mvt Code** tab in **Item Details** section. Select **Movement Code*** and other fields.

4. Click on **Order Data** tab in **Item Details** section. Click on **Add New** button. **Select Order Items** window will appear. Select the order and click on **OK**.

![](/images/pogr-additem-details.png)

5. Enter **QtyRecd** if this field is blank.

![](/images/pogr-additem-details-qtyrecd.png)

6. Click on **Quantity** tab in **Item Details** section. Enter **Quantity** (It should be same as given in selected order.)

7. Click on **Save** button.


### Material Voucher Against LPO(GR)

Follow same steps as given in material voucher against PO. Only difference is that you have to select Local Purchase Order(LPO) instead of Purchase order(PO) in Ref Document Type column.

### Material Voucher Against JWO(GR)

**STEP-1**: Select **Voucher Type** (Goods Receipt), **Ref Document Type** (Job Work Order), **Tax Type** and **Department**.

![](/images/jwogr-step1.png)

**STEP-2**: Click on **Select Document**. Select Job Work Order window will appear. Select the job work order and click on **OK**.

![](/images/jwogr-select.png)

**STEP-3**: Click on **Execute Selection**. **Select** window will appear. Select the items and enter **Qty** if not given. Click on **OK**.

![](/images/jwogr-selection.png)

Entries will fill automatically.

![](/images/jwogr-selection-autofilled.png)

**STEP-4**: Click on **Pricing** tab in **Header** section. Select **Procedure** and **Slab**.

**STEP-5**: Click on **Document** tab in **Header** section. Enter **Challan No.** and **GR No.** Select **Challan Date, Challan Received** and **Transporter**.

![](/images/jwogr-headerdoc.png)

**STEP-6**: Click on **BOM** tab in **Item Details**. Click on **Add New**. Select **Item Code** under **Material** tab in **BOM** section.

![](/images/jwogr-headerdoc-itemdetail.png)

**STEP-7**: Click on **Mvt Code** in **BOM** section. Select **Movement Code***.

![](/images/jwogr-headerdoc-itemdetail-bom.png)

**STEP-8**: Click on **Quantity**. Enter **Qty**.

![](/images/jwogr-headerdoc-itemdetail-bom-qty.png)

**STEP-9**: Click on **Save** button.

![](/images/jwogr-save.png)

**Note:** If you want to add item manually, follow the given steps:
1. Select **Voucher Type (Goods Receive)**, **Ref Document Type, Tax Type** and **Department**.

2. Click on **Add New** button in **Item List** section. New row will appear.

3. Select **Item Code** in **Material** tab in Item Detail section.

4. Click on **Mvt Code** tab in **Item Details** section. Select **Movement Code** and enter **Manufacturing Charges**.

 ![](/images/jwogr-item-manual.png)

5. Click on **Quantity** tab in **Item Details** and enter **Qty**.

6. Click on **Location** tab in **Item Details** and select **Storage Department**.

7. Click on **Order Data** tab. Click on **Add New** button. Select Oder Items list will appear. Select the order and click on **OK**.

![](/images/jwogr-item-manual-addnew.png)

8. Enter Quantity Received in **QtyRecd** column.

![](/images/jwogr-item-manual-addnew-qtyrecd.png)

9. Click on **Save** button.

![](/images/jwogr-item-manual-addnew-qtyrecd-save.png)

### Material Voucher Against Other(GR)

**STEP-1**: Select **Voucher Type** (Goods Receipt), **Ref Document Type** (Other), **Tax Type** and **Department**.

![](/images/othergr.png)

**STEP-2**: Click on **Add New** button in **Item List** section. New row will appear.

![](/images/othergr-addnew.png)

**STEP-3**: Click on **General** tab in **Header** section and fill the details.

**STEP-4**: Click on **Pricing** tab in **Header** section. Select **Procedure** and **Slab**.

**STEP-5**: Click on **Document** tab in **Header** section. Enter **Challan No.** and **GR No.**. Select **Challan Date, Challan Received** and **Transporter**.

![](/images/othergr-addnew-document.png)

**STEP-6**: Select **Item Code** under **Material** tab in **Item Details** section.

**STEP-7**: Select **Movement Code** and other details under **Mvt Code** tab in **Item Details** section.

**STEP-8**: Select **Qty** under **Quantity** tab in **Item Details** section.

![](/images/othergr-addnew-quantity.png)

**STEP-9**: Select **Storage Location** under **Location** tab in **Item Details** section if available.

**STEP-10**: Click on **Save** button.

![](/images/othergr-addnew-save.png)

### Material Voucher Against OD Note(GI)

**STEP-1**: Select **Voucher Type** (Goods Issue), **Ref Document Type** (Outgoing Delivery Note), **Tax Type** and **Department**.

**STEP-2**: Click on **Select Document**. **Select Delivery Note** window will appear.

 ![](/images/odnotegi.png)

**STEP-3**: Select the delivery note and Click on **OK**.

**STEP-4**: Click on **Execute Selection**. Entries will fill automatically.

**STEP-5**: Click on **Location** tab and select **Storage Department**.

**STEP-6**: Click on **Save** button.

![](/images/odnotegi-save.png)



# Material Voucher Items

## List

Click on **Voucher Items->Material Voucher Items**. Material Voucher Items list will appear.

![](/images/matvoucher-item-list.png)
 
## Edit

**Step-1:** Select the material voucher items from list that you want to edit.

**Step-2:** Right click on it and select **Edit Material Voucher** option.

![](/images/matvoucher-item-edit.png)

**Step-3:** Edit details and click on **Save**. 








