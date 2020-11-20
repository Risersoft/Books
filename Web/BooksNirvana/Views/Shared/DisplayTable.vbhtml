@imports System
@imports System.Data
@ModelType DataSet


@Using (Html.BeginForm("Index", "frmImport", FormMethod.Post, New With {.id = "frmTable", .name = "frmTable"}))
@<div>
    <div class="form-group">
        <div class="col-md-offset-1 col-md-10 table-overflow" style="margin-top:8px">
            <label class="table-label">Table Content</label>
            <Table Class="table table-bordered detailcolumn filtertable">
                <tr>
                    @For Each col As System.Data.DataColumn In Model.Tables(0).Columns
                       @<th>@col.ColumnName</th>
                    Next
                </tr>

                @For Each row As System.Data.DataRow In Model.Tables(0).Select
                   @<tr>
                       @For Each col As System.Data.DataColumn In Model.Tables(0).Columns
                          @<td>@row(col.ColumnName)</td>
                       Next

                   </tr>
                Next
            </Table>

        </div>
      </div>
     <div Class="form-group" style="margin-top:8px">
         <div Class="col-md-offset-1 col-md-10" style="margin-top:50px;">
             <input type="submit" id="btnSave" value="Save" Class="btn btn-primary" />
         </div>
     </div>
</div>
End Using
@section Scripts

    <script type="text/javascript">
        $(document).ready(function () {
            alert();
        });
    </script>


    
end section