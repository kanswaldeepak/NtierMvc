

function getRoleAssignedURL() {
    var MainMenu = '';
    var x = document.getElementById("AMMainMenuSearch");
    for (var i = 0; i < x.options.length; i++) {
        if (x.options[i].selected == true) {
            //alert(x.options[i].value);
            MainMenu = MainMenu + x.options[i].value + ',';
        }
    }
    MainMenu = MainMenu.substring(0, MainMenu.length - 1);

    var SubMenu = '';
    var x = document.getElementById("AMSubMenuSearch");
    for (var i = 0; i < x.options.length; i++) {
        if (x.options[i].selected == true) {
            //alert(x.options[i].value);
            SubMenu = SubMenu + x.options[i].value + ',';
        }
    }
    SubMenu = SubMenu.substring(0, SubMenu.length - 1);

    var DeptName = $('#AMDeptNameSearch').val();
    var Access = $('#AMAccessSearch').val();

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
            "data": { deptName: DeptName, mainMenu: MainMenu, subMenu: SubMenu, access: Access },
            "dataSrc": ""
        },
        'order': [[1, "asc"]],
        columns: [
            { title: "ID", "data": "ID", "name": "ID", "autoWidth": false, "visible": false },
            { title: "SNo", "data": "SNo", "name": "SNo", "autoWidth": false, "visible": true },
            { title: "Emp Id", "data": "EmpId", "name": "EmpId", "autoWidth": false, "visible": false },
            { title: "Emp Code", "data": "EmpCode", "name": "EmpCode", "autoWidth": false, "visible": true },
            { title: "Emp Name", "data": "EmpName", "name": "EmpName", "autoWidth": false, "visible": true },
            { title: "Dept Name", "data": "DeptName", "name": "DeptName", "autoWidth": true, "visible": true },
            { title: "Main Menu", "data": "MainMenu", "name": "MainMenu", "autoWidth": true, "visible": true },
            { title: "Sub Menu", "data": "SubMenu", "name": "SubMenu", "autoWidth": true },
            { title: "Access", "data": "Access", "name": "Access", "autoWidth": true }
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
    var role = $('#AMDept').val();
    var EmpId = $('#AMEmployee').val();
    var mainMenu = $('#AMMainMenu').val();
    var subMenu = $('#AMSubMenu').val();
    var readWrite = $('#AMReadWrite').val();

    ShowLoadder();
    var Status = false;
    Status = GetFormValidationStatus("#formSaveRoleAuthentication");

    if (!Status) {
        alert("Kindly Fill all mandatory fields");
        HideLoadder();
    }
    else {
        $.ajax({
            url: window.SaveRoleAssigns, type: 'POST', data: { Role: role, EmpId: EmpId, MainMenu: mainMenu, SubMenu: subMenu, ReadWrite: readWrite }, ContentType: undefined,
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
            , error: function (res) { showHttpErr(res); HideLoadder(); }
        })
    }
}
