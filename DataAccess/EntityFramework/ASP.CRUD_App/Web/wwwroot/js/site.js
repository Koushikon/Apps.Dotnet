
$(function () {

    // DataTable configure
    let table = new DataTable('#tableCRUD', {
        columnDefs: [
            {
                orderable: false,
                render: DataTable.render.select(),
                targets: 0
            }
        ],
        paging: true,
        searching: false,
        lengthChange: false,
        ordering: false,
        pageLength: 5,
        language: {
            paginate: {
                previous: 'Previous',
                next: 'Next'
            }
        },
        layout: {
            bottomEnd: {
                paging: {
                    firstLast: false
                }
            }
        },
        select: {
            style: 'multi',
            selector: 'td:first-child'
        }
    });


    // Selected Ids
    let selectedIds = [];

    // Selection Delete button event
    $(document).on('click', '#btnDeleteRows', function () {

        let selectedRows = table.rows({ selected: true }).nodes();
        selectedIds = [];

        $(selectedRows).each(function () {
            let empId = $(this).find('.emp-id').val();
            if (empId) {
                selectedIds.push(empId);
            }
        });

        if (selectedIds.length > 0) {
            $('#deleteModal').modal('show');
        } else {
            alert('Please select at least one record to delete.');
        }
    });


    // Specific Record Delete button event
    $(document).on('click', '.btn-Delete-Single', function () {
        selectedIds = [];

        if (this.hasAttribute('data-bs-eid')) {
            const eid = this.getAttribute('data-bs-eid');
            if (eid) {
                selectedIds.push(eid);
            }
        }
    });


    // Deletion modal Delete button event
    $(document).on('click', '#deleteModal #btnDelete', function () {

        if (selectedIds.length > 0) {

            // Construct ids as query string
            let queryString = selectedIds.map(id => `ids=${id}`).join('&');

            $.ajax({
                url: `/Home/Delete?${queryString}`,
                method: 'DELETE',
                success: function (response) {
                    if (response.success) {
                        window.location.href = '/';
                    } else {
                        alert('Failed to delete records.');
                    }
                },
                error: function () {
                    alert('An error occurred while processing your request.');
                }
            });
        }
    });


    // Show Upsert Modal configure
    const upsertModal = document.getElementById('upsertModal')
    if (upsertModal) {
        upsertModal.addEventListener('show.bs.modal', event => {

            const button = event.relatedTarget
            const recipient = button.getAttribute('data-bs-whatever')
            const eid = button.getAttribute('data-bs-eid')

            const modalTitle = upsertModal.querySelector('.modal-title')
            modalTitle.textContent = recipient

            const elementId = upsertModal.querySelector('#hdnEmpID')
            elementId.value = eid;

            const btnElement = upsertModal.querySelector('#btnSave');
            if (parseInt(eid) > 0) {
                btnElement.textContent = "Save";
                btnElement.classList.remove('btn-success');
                btnElement.classList.add('btn-info');
            } else {
                btnElement.textContent = "Add";
                btnElement.classList.remove('btn-info');
                btnElement.classList.add('btn-success');
            }
        })
    }
});