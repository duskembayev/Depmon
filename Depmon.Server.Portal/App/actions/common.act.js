module.exports = {
    onAjaxFailed : function (jqXHR, textStatus, errorThrown) {
        // TODO Подцепить визуальное отображение, общее для всех
        alert('textStatus');
    }
};