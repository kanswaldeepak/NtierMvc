

function getRoleAssignedURL() {
    var AMRoleId = $('#AMRole').val();
    var AMMainMenuId = $('#AMSubMenu').val();
    var AMSubMenuId = $('#AMSubMenu').val();

    $("#RoleURLtable").DataTable().destroy();

    var req = {
        "processing": true,
        "language": {
            processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
        },
        "serverSide": true,
        "paging": true,
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        "pageLength": 5,
        "searching": true,
        "filter": true,
        "ajax": {
            "url": window.GetRoleURLDetails,
            "type": "POST",
            "datatype": "json",
            "data": { AMRoleID: AMRoleId, AMMainMenuID: AMMainMenuId, AMSubMenuID: AMSubMenuId },
            "dataSrc": ""
        },
        'order': [[1, "asc"]],
        columns: [
            { title: "ID", "data": "ID", "name": "ID", "autoWidth": false, "visible": false },
            { title: "SNo", "data": "SNo", "name": "SNo", "autoWidth": false, "visible": true },
            { title: "Dept Name", "data": "DeptName", "name": "DeptName", "autoWidth": true, "visible": true },
            { title: "Main Menu", "data": "MainMenu", "name": "MainMenu", "autoWidth": true, "visible": true },
            { title: "Sub Menu", "data": "SubMenu", "name": "SubMenu", "autoWidth": true }
        ],
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
        },
        "drawCallback": function (settings) {
        },
        "footerCallback": function (row, data, start, end, display) {

        }
    }
    $("#RoleURLtable").DataTable(req);
    $("#RoleURLtable tbody").show();
}

function SaveRoleAssignDetails() {
    var frm = $("#formSaveRoleAuthentication");
    var role = $('#AMRole').val();
    var mainMenu = $('#AMMainMenu').val();
    var subMenu = $('#AMSubMenu').val();

    ShowLoadder();
    var Status = false;
    Status = GetFormValidationStatus("#formSaveRoleAuthentication");

    if (!Status) {
        alert("Kindly Fill all mandatory fields");
        HideLoadder();
    }
    else {
        $.ajax({
            url: window.SaveRoleAssigns, type: 'POST', data: { Role: role, MainMenu: mainMenu, SubMenu: subMenu }, ContentType: undefined ,
            success:
                function (res) {
                    if (res == 'Inserted Successfully!') {
                        alert(res);
                        HideLoadder();
                    }
                    else {
                        alert(res);
                        HideLoadder();
                    }
                }
            , error: function (res) { showHttpErr(res); HideLoadder();}
        })
    }
}
