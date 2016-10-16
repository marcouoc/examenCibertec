function getModalContent(url) {
    $.get(url, function (data) {
        $('.modal-body').html(data);
    });
}

function closeModal(option) {
    $("button[data-dismiss='modal']").click();
    $('.modal-body').html('');
    modifyAlertsClasses(option);
}

function modifyAlertsClasses(option) {
    $('#successMessage').addClass('hidden');
    $('#editMessage').addClass('hidden');
    $('#deleteMessage').addClass('hidden');
    if (option === 'create')
        $('#successMessage').removeClass('hidden');
    else if (option === 'edit')
        $('#editMessage').removeClass('hidden');
    else if (option === 'delete')
        $('#deleteMessage').removeClass('hidden');
}