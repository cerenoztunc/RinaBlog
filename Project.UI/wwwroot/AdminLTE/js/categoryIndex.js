$(document).ready(function () {

/* DataTables start here. */

    $('#categoriesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        //"order": [[6, "desc"]],
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
                        url: '/Admin/Category/GetAllCategories/',
                        contentType: 'application/json',
                        beforeSend: function () {
                            $('#categoriesTable').hide(); //table'ı gizledik ve
                            $('.spinner-border').show(); //table'ın hemen üstüne koyduğumuz yükleniyor görselini görünür hale getirdik
                        },
                        success: function (data) {
                            const categoryListDto = jQuery.parseJSON(data);
                            if (categoryListDto.ResultStatus === 0) {
                                let tableBody = "";
                                $.each(categoryListDto.Categories.$values, function (index, category) {
                                    tableBody +=
                                        `<tr>
                                            <td>${category.ID}</td>
                                            <td>${category.Name}</td>
                                            <td>${category.Description}</td>
                                            <td>${convertFirstLetterToUpperCase(category.IsActive.toString())}</td>
                                            <td>${convertFirstLetterToUpperCase(category.IsDeleted.toString())}</td>
                                            <td>${category.Note}</td>
                                            <td>${convertToShortDate(category.CreatedDate)}</td>
                                            <td>${category.CreatedByName}</td>
                                            <td>${convertToShortDate(category.ModifiedDate)}</td>
                                            <td>${category.ModifiedByName}</td>
                                            <td>
                                                <button class="btn btn-primary btn-sm btn-update" data-id="${category.ID}">
                                                    <span class="fas fa-edit"></span>
                                                </button>
                                                <button class="btn btn-danger btn-sm btn-delete" data-id="${category.ID}">
                                                    <span class="fas fa-minus-circle"></span>
                                                </button>
                                            </td>
                                        </tr>`;
                                });
                                $('#categoriesTable > tbody').replaceWith(tableBody); //tbody html'i bizim güncellenmiş olan tableBody ile yer değiştirecek..
                                $('.spinner-border').hide();
                                $('#categoriesTable').fadeIn(1400);
                            } else {
                                toastr.error(`${categoryListDto.Message}`, 'İşlem Başarısız!');
                            }

                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#categoriesTable').fadeIn(1000);
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

/* Ajax GET / Getting the _CategoryAddPartial as Modal Form start here */

    $(function () {
        const url = '/Admin/Category/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

    /* Ajax POST / Posting the FormData as CategoryAddDto starts from here.*/

        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-category-add');
            const actionUrl = form.attr('action'); //burası bize url oluşturacak..Yani bu formun hangi url'e post edileceğini belirtecek..
            const dataToSend = form.serialize(); //form içindeki veriyi CategoryAddDto olarak dönüştürdük..post işlemin Neyin gönderileceğini söyleyeceğiz..
            $.post(actionUrl, dataToSend).done(function (data) {
                const categoryAddAjaxModel = jQuery.parseJSON(data); //C# tarafındaki viewmodel gibi bir model almış olduk..
                const newFormBody = $('.modal-body', categoryAddAjaxModel.CategoryAddPartial); //form modal içerisine doldurduk alınan bilgileri..bu aynı zamanda kullanıcının yaptığı hatalar varsa kullanıcıyı bilgilendirmeyi sağlayacak..
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody); //yeni alınan body ile eskisinin yerini değiştirdik..
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True'; //geçerli olup olmadığının kontrolünü sağlıyoruz. eğer yakaladığımız IsValid attribute'u true ise hata yo demektir. ancak false ise isValid değişkenine false atanacağından hatalar döndürülecek..
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    const newTableRow =
                        `<tr name="${categoryAddAjaxModel.CategoryDto.Category.ID}">
                                <td>${categoryAddAjaxModel.CategoryDto.Category.ID}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Name}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Description}</td>
                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsActive.toString())}</td>
                                <td>${convertFirstLetterToUpperCase(categoryAddAjaxModel.CategoryDto.Category.IsDeleted.toString())}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.Note}</td>
                                <td>${convertToShortDate(categoryAddAjaxModel.CategoryDto.Category.CreatedDate)}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.CreatedByName}</td>
                                <td>${convertToShortDate(categoryAddAjaxModel.CategoryDto.Category.ModifiedDate)}</td>
                                <td>${categoryAddAjaxModel.CategoryDto.Category.ModifiedByName}</td>
                                <td>
                                     <button class="btn btn-primary btn-sm btn-update" data-id="${categoryAddAjaxModel.CategoryDto.Category.ID}">
                                           <span class="fas fa-edit"></span>
                                     </button>
                                     <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAddAjaxModel.CategoryDto.Category.ID}">
                                            <span class="fas fa-minus-circle"></span>
                                     </button>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow); //artık yeni bir row oluşturup içine attık..
                    newTableRowObject.hide(); //yeni row'u gizledik..
                    $('#categoriesTable').append(newTableRowObject); //bize dönen objeyi yeni row'un içine doldurup tablonun sonuna ekledik..
                    newTableRowObject.fadeIn(3500); //efektli bir şekilde 3 buçuk saniyede görünmesini sağladık..
                    toastr.success(`${categoryAddAjaxModel.CategoryDto.Message}`, 'Başarılı İşlem!') //sayfanın sağ üst köşesinde toastr mesajını gösterdik..
                } else {
                    let summaryText = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text(); //her bir ul içinde bulunan li'nin yazısını aldık ve summaryText içine topladık. Bunu yaparekn
                        summaryText = `*${text}\n`; //başında yıldız çıkmasını ve her bir uyarıdan sonra alt satıra geçmesini sağladık..
                    });
                    toastr.warning(summaryText);
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
                    url: '/Admin/Category/Delete/',
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
        const url = '/Admin/Category/Update/';
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
            const form = $('#form-category-update');
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