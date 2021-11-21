$(document).ready(function () {
    //DataTable
    $('#articlesTable').DataTable({
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
        },
        "order":[[4,"desc"]]

    });
    //DataTable

    //Chart.js
    
    $.get('/Admin/Article/GetAllByView/?isAscending=false&takeSize=10',
        function (data) {
        const articleResult = jQuery.parseJSON(data);
        let viewCountContext = $('#viewCountChart');
        let viewCountChart = new Chart(viewCountContext, {
            type: 'bar',
            data: {
                labels: articleResult.$values.map(article => article.Title),
                datasets: [
                    {
                        label: 'Okunma Sayısı',
                        data: articleResult.$values.map(article => article.ViewsCount),
                        backgroundColor: '#3F3351',
                        hoverBorderWidth: 4,
                        hoverBorderColor: '#87AAAA'
                    },
                    {
                        label: 'Yorum Sayısı',
                        data: articleResult.$values.map(article => article.CommentCount),
                        backgroundColor: '#864879',
                        hoverBorderWidth: 4,
                        hoverBorderColor: '#87AAAA'
                    }
                ]
            },
            options: {
                plugins: {
                    legend: {
                        labels: {
                            font: {
                                size: 19
                            }
                        }
                    }
                }
            }
        });
    });
});