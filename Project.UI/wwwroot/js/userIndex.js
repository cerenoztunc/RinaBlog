﻿$(document).ready(function () {

/* DataTables start here. */

   const dataTable = $('#usersTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {

                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/User/GetAllUsers/',
                        contentType: 'application/json',
                        beforeSend: function () {
                            $('#usersTable').hide(); //table'ı gizledik ve
                            $('.spinner-border').show(); //table'ın hemen üstüne koyduğumuz yükleniyor görselini görünür hale getirdik
                        },
                        success: function (data) {
                            const userListDto = jQuery.parseJSON(data);
                            if (userListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(userListDto.Users.$values, function (index, user) {
                                    tableBody +=
                                        `<tr>
                                            <td>${user.Id}</td>
                                            <td>${user.UserName}</td>
                                            <td>${user.Email}</td>
                                            <td>${user.PhoneNumber}</td>
                                            <td>${user.Picture}</td>
                                            
                                            <td>
                                                <button class="btn btn-primary btn-sm btn-update" data-id="${user.Id}">
                                                    <span class="fas fa-edit"></span>
                                                </button>
                                                <button class="btn btn-danger btn-sm btn-delete" data-id="${user.Id}">
                                                    <span class="fas fa-minus-circle"></span>
                                                </button>
                                            </td>
                                        </tr>`;
                                });
                                $('#usersTable > tbody').replaceWith(tableBody); //tbody html'i bizim güncellenmiş olan tableBody ile yer değiştirecek..
                                $('.spinner-border').hide();
                                $('#usersTable').fadeIn(1400);
                            } else {
                                toastr.error(`${userListDto.Message}`, 'İşlem Başarısız!');
                            }

                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#usersTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "İşleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });

/* DataTables ends here */

/* Ajax GET / Getting the _UserAddPartial as Modal Form start here */

    $(function () {
        const url = '/Admin/User/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

    /* Ajax POST / Posting the FormData as UserAddDto starts from here.*/

        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-user-add');
            const actionUrl = form.attr('action'); //burası bize url oluşturacak..Yani bu formun hangi url'e post edileceğini belirtecek..
            const dataToSend = new FormData(form.get(0));
            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType:false,
                success: function (data) {
                    const userAddAjaxModel = jQuery.parseJSON(data); //C# tarafındaki viewmodel gibi bir model almış olduk..
                    const newFormBody = $('.modal-body', userAddAjaxModel.UserddPartial); //form modal içerisine doldurduk alınan bilgileri..bu aynı zamanda kullanıcının yaptığı hatalar varsa kullanıcıyı bilgilendirmeyi sağlayacak..
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody); //yeni alınan body ile eskisinin yerini değiştirdik..
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True'; //geçerli olup olmadığının kontrolünü sağlıyoruz. eğer yakaladığımız IsValid attribute'u true ise hata yo demektir. ancak false ise isValid değişkenine false atanacağından hatalar döndürülecek..
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        dataTable.row.add([
                            userAddAjaxModel.UserDto.User.Id,
                            userAddAjaxModel.UserDto.User.UserName,
                            userAddAjaxModel.UserDto.User.Email,
                            userAddAjaxModel.UserDto.User.PhoneNumber,
                            `<img src="/img/${userAddAjaxModel.UserDto.User.Picture}" alt="${userAddAjaxModel.UserDto.User.UserName}" style="max-height: 50px; max-width: 50px; "/>`,
                            `<td>
                                    <button class="btn btn-primary btn-sm btn-update" data-id="userAddAjaxModel.UserDto.User.Id">
                                        <span class="fas fa-edit"></span>
                                    </button>
                                    <button class="btn btn-danger btn-sm btn-delete" data-id="userAddAjaxModel.UserDto.User.Id">
                                        <span class="fas fa-minus-circle"></span>
                                    </button>
                            </td>`
                        ]).draw();
                        toastr.success(`${userAddAjaxModel.UserDto.Message}`, 'Başarılı İşlem!') //sayfanın sağ üst köşesinde toastr mesajını gösterdik..
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text(); //her bir ul içinde bulunan li'nin yazısını aldık ve summaryText içine topladık. Bunu yaparekn
                            summaryText = `*${text}\n`; //başında yıldız çıkmasını ve her bir uyarıdan sonra alt satıra geçmesini sağladık..
                        });
                        toastr.warning(summaryText);
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
    });

/* Ajax POST / Deleting a Category starts from here*/

    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const categoryName = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'Silmek istediğinize emin misiniz?',
            text: `${categoryName} adlı kategori silinecektir!`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, silmek istiyorum!',
            cancelButtonText: 'Hayır, silmek istemiyorum!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { categoryID: id },
                    url: '/Admin/User/Delete/',
                    success: function (data) {
                        const categoryDto = jQuery.parseJSON(data);
                        if (categoryDto.ResultStatus === 0) {
                            Swal.fire(
                                'Silindi!',
                                `${categoryDto.Category.Name} adlı kategori başarıyla silinmiştir!`,
                                'success'
                            );

                            tableRow.fadeOut(3500);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: `${categoryDto.Message}`
                            });
                        }
                    },
                    error: function (err) {
                        console.log(err);
                        toastr.error(`${err.responseText}`, "Hata!");
                    }
                });
            }
        });
    });
    $(function () {
        const url = '/Admin/User/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            
            const id = $(this).attr('data-id'); 

            $.get(url, { categoryID: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');

            }).fail(function () {
                toastr.error("Bir hata oluştu");
            });
        });

    /* Ajax POST / Updating a Category starts from here */

        placeHolderDiv.on('click', '#btnUpdate', function (event) {
            event.preventDefault();
            const form = $('#form-user-update');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {

                const categoryUpdateAjaxModel = jQuery.parseJSON(data);
                const newFormBody = $('.modal-body', categoryUpdateAjaxModel.CategoryUpdatePartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';

                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');

                    const newTableRow =
                        `<tr name="${categoryUpdateAjaxModel.CategoryDto.Category.ID}">
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.ID}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.Name}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.Description}</td>
                                <td>${convertFirstLetterToUpperCase(categoryUpdateAjaxModel.CategoryDto.Category.IsActive.toString())}</td>
                                <td>${convertFirstLetterToUpperCase(categoryUpdateAjaxModel.CategoryDto.Category.IsDeleted.toString())}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.Note}</td>
                                <td>${convertToShortDate(categoryUpdateAjaxModel.CategoryDto.Category.CreatedDate)}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.CreatedByName}</td>
                                <td>${convertToShortDate(categoryUpdateAjaxModel.CategoryDto.Category.ModifiedDate)}</td>
                                <td>${categoryUpdateAjaxModel.CategoryDto.Category.ModifiedByName}</td>
                                <td>
                                     <button class="btn btn-primary btn-sm btn-update" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.ID}">
                                           <span class="fas fa-edit"></span>
                                     </button>
                                     <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.ID}">
                                            <span class="fas fa-minus-circle"></span>
                                     </button>
                                </td>
                        </tr>`;
                    const newTableRowObject = $(newTableRow);
                    const categoryTableRow = $(`[name="${categoryUpdateAjaxModel.CategoryDto.Category.ID}"]`); //güncellenen kategorinin yerini bilmek ve bu şekilde table'da yerine koyabilmek için..
                    newTableRowObject.hide();
                    categoryTableRow.replaceWith(newTableRowObject);
                    newTableRowObject.fadeIn(3500);
                    toastr.success(`${categoryUpdateAjaxModel.CategoryDto.Message}`, "Başarılı İşlem!");
                } else {
                    let summaryText = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text(); //her bir ul içinde bulunan li'nin yazısını aldık ve summaryText içine topladık. Bunu yaparken
                        summaryText = `*${text}\n`; //başında yıldız çıkmasını ve her bir uyarıdan sonra alt satıra geçmesini sağladık..
                    });
                    toastr.warning(summaryText);
                }

            }).fail(function (response) {
                console.log(response);
            });
        });

    });
});