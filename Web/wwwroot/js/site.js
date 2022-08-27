$(document).ready(function () {
    // Run on note modal popup and clear previous creation
    $("#newNoteModal").on('show.bs.modal', function () {
        $("#newNoteBody").val("");
        loadCategories();
    });

    // Get Text for the note Id currently set in the model
    $("#displayNoteModal").on('show.bs.modal', function () {
        $("#deleteAlert").hide();
    });

    // Run on category modal popup and focus on text box.
    $("#newCategoryModal").on('show.bs.modal', function () {
        $("#category-name").focus();
    });

    // Create category via ajax call to the URL API
    $("#createCategory").on("click", function () {
        var categoryName = $("#category-name").val();
        $.ajax({
            url: apiUrl + "Categories",
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                name: categoryName
            }),
            success: function () {
                alert("Category Created", "success");
                $('#newCategoryModal').modal('hide');
            },
            error: function () {
                alert("Error creating category, please try again.");
            }
        });
    });

    // Display clicked note in it's own modal
    $(document).on("click", ".list-group-item", function () {
        var id = $(this).attr("data-id");
        displayNote(id);
        $("#displayNoteModal").modal('show');
    });

    $("#displayNoteDelete").on("click", function () {
        $("#deleteAlert").show();
    });

    $("#displayNoteDeleteYes").on("click", function () {
        let id = $("#displayNoteId").val();

        $.ajax({
            url: apiUrl + "Notes/" + id,
            type: "DELETE",
            success: function () {
                alert("Note Deleted", "success");
                $('#displayNoteModal').modal('hide');
                loadNotes();
            },
            error: function () {
                alert("Error deleting note, please try again.");
            }
        });
    });

    // Create a new note in the system
    $("#newNoteCreate").on("click", function () {
        let body = $("#newNoteBody").val();

        // Loop through the selected categories 
        var noteCategories = [];
        $('#newNoteCategories :selected').each(function () {
            var arr = { "categoryId" : $(this).val() };
            noteCategories.push(arr);
        });

        $.ajax({
            url: apiUrl + "Notes",
            type: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                body: body,
                noteCategories: noteCategories
            }),
            success: function (data) {
                alert("Note Created", "success");
                $('#newNoteModal').modal('hide');
                addNoteItem(data.id, data.body);
            },
            error: function () {
                alert("Error deleting note, please try again.");
            }
        });
    });

    $("#displayNoteDeleteNo").on("click", function () {
        $("#deleteAlert").hide();
    });

    // Retreive the notes from the API in html form
    function displayNote(id) {
        $("#noteHtml,#noteCategories").html("");
        $("#displayNoteId").val(id);

        $.ajax({
            url: apiUrl + "Notes/" + id + "/html",
            type: "GET",
            dataType: 'json',
            success: function (data) {
                $("#noteHtml").html(data.body);
                $("#noteDate").html(data.date);
                $.each(data.noteCategories, function (i, item) {
                    $("#noteCategories").append(item.category.name + "<br>");
                });
            },
            error: function () {
                alert("Error retrieving note, please refresh page.");
            }
        });
    }

    // Retreive the notes from the API
    function loadNotes() {
        $(".note-group").empty();

        $.ajax({
            url: apiUrl + "Notes",
            type: "GET",
            dataType: 'json',
            success: function (data) {
                $.each(data, function (i, item) {
                    addNoteItem(item.id, item.body);
                });
            },
            error: function () {
                alert("Error retrieving notes, please refresh page.");
            }
        });
    }

    // Get the categories from the API and load them into the dropdown to select from
    function loadCategories() {
        $("#newNoteCategories").empty();

        $.ajax({
            url: apiUrl + "Categories",
            type: "GET",
            dataType: 'json',
            success: function (data) {
                $.each(data, function (i, item) {
                    addCategoryItem(item.id, item.name);
                });
            },
            error: function () {
                alert("Error retrieving notes, please refresh page.");
            }
        });
    }

    function addNoteItem(id, text) {
        var listItem = '<button type="button" class="list-group-item list-group-item-action" data-id="' + id + '">' + text + '</button>';
        $(".note-group").append(listItem);
    }

    function addCategoryItem(id, text) {
        var listItem = '<option value="' + id + '">' + text + '</option>';
        $("#newNoteCategories").append(listItem);
    }

    // Allow text and type of alert to passed and displayed for reusable alerts.
    function alert(text, type) {
        $(".alert-" + type + "-message").html(text);
        $(".alert-" + type).show();

        setTimeout(function () {
            $(".alert").hide();
        }, 3000);
    }

    loadNotes();
});

// URL to API, change if setup differently
var apiUrl = "https://localhost:7245/";