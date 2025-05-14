// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


        document.getElementById('SelectedCategory').addEventListener('change', function () {
            const selectedCategory = this.value;
        const subCategorySelect = document.getElementById('SubCategories');

        subCategorySelect.innerHTML = '';

        if (selectedCategory) {
            fetch(`/Announcements/GetSubcategories?category=${encodeURIComponent(selectedCategory)}`)
                .then(response => response.json())
                .then(data => {
                    subCategorySelect.innerHTML = '<option value="">-- Select Subcategory --</option>';
                    data.forEach(sub => {
                        const option = document.createElement('option');
                        option.value = sub;
                        option.textContent = sub;
                        subCategorySelect.appendChild(option);
                    });
                });
            }
        });
