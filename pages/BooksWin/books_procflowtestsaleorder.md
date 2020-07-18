---
title: Testing Against Sales Order
keywords: Testing Against Sales Order
sidebar: bookswin_sidebar
permalink: books-nirvana-win/testing-against-sales-order.html
folder: BooksWin
hide_sidebar: false
comments: false
---

# Testing Against Sales Order

If we send any item out of campus for testing, then the item show in transit(TR) quantity of delivery campus.

**For issue the item for testing, we have to create:**

OD Note (Goods Issue) for Testing Against Sales Order with 313 mvt code -> Material Voucher

After testing, we receive to receive this testing item. Now this item is in Unreserved(UR) quantity of delivery campus.

**For receive the testing item, we have to create:**

Material Voucher (Goods Receive) against Material Voucher (313 mvt code)
