let productId;

    async function showEditModal(element) {
        $('#editModal').modal('show');
        productId = $(element).data("id");
        try {
            let product = await getProductDetails(productId);
            console.log(product);

            $('#editName').val(product.name);
            $('#editDescription').val(product.description);
        } catch (error) {
            console.error('Error fetching product details:', error);
        }
    }

    async function showAddModal() {
        $('#addModal').modal('show');
    }

    async function saveEditModal(element) {
        let id = productId;
        let name = $('#editName').val();
        let description = $('#editDescription').val();
        let product = {
            id: id,
            name: name,
            description: description
        };

        await updateProductDetails(product);
        $('#editModal').modal('hide');
        window.location.reload();
    }

    async function saveAddModal(element) {
        let name = $('#addName').val();
        let description = $('#addDescription').val();
        let product = {
            id: null,
            name: name,
            description: description
        };

        await addProductDetails(product);
        $('#addModal').modal('hide');
        window.location.reload();
    }

    function hideEditModal(element) {
        $('#editModal').modal('hide');
    }

    function hideAddModal(element) {
        $('#addModal').modal('hide');
    }

    function getProductDetails(id) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: `http://localhost:5155/api/products/${id}`,
                type: 'GET',
                success: function (productDto) {
                    resolve(productDto);
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching product details:', error);
                    reject(error);
                }
            });
        });
    }

    function updateProductDetails(product) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: `http://localhost:5155/api/products`,
                type: 'PUT',
                contentType: 'application/json', 
                data: JSON.stringify(product),
                success: function () {
                    resolve(); 
                },
                error: function (xhr, status, error) {
                    console.error('Error updating product details:', error);
                    reject(error);
                }
            });
        });
    }

    async function addProductDetails(product) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: `http://localhost:5155/api/products`,
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(product),
                success: function (addedProductDto) {
                    resolve(addedProductDto);
                },
                error: function (xhr, status, error) {
                    console.error('Error adding product:', error);
                    reject(error);
                }
            });
        });
    }