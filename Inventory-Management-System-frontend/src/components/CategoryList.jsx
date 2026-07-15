import { useState, useEffect } from 'react';
// Corrected import path to point to /services/
import { getCategories, deleteCategory, updateCategory } from '../services/categoryService'; 
import CategoryForm from './CategoryForm';

const CategoryList = () => {
    const [categories, setCategories] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // --- Inline Edit States ---
    const [editingId, setEditingId] = useState(null);
    const [editName, setEditName] = useState('');
    const [editDescription, setEditDescription] = useState('');
    const [isUpdating, setIsUpdating] = useState(false);

    const fetchCategories = async () => {
        setLoading(true);
        try {
            const response = await getCategories();
            setCategories(response.data); 
            setError(null);
        } catch (err) {
            console.error("API Error:", err);
            setError('Failed to load categories.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchCategories();
    }, []); 

    const handleDelete = async (id) => {
        if (!window.confirm("Are you sure you want to delete this category?")) {
            return;
        }
        try {
            await deleteCategory(id);
            setCategories(categories.filter(c => c.id !== id));
        } catch (err) {
            console.error("Failed to delete category:", err);
            alert("Could not delete category. It might be linked to existing products!");
        }
    };

    const startEdit = (category) => {
        setEditingId(category.id);
        setEditName(category.name);
        setEditDescription(category.description || '');
    };

    const cancelEdit = () => {
        setEditingId(null);
        setEditName('');
        setEditDescription('');
    };

    const handleUpdate = async (id) => {
        if (!editName.trim()) {
            alert("Category name is required.");
            return;
        }

        setIsUpdating(true);
        try {
            
            const updatedData = { id: id, name: editName, description: editDescription };
            await updateCategory(id, updatedData);

            setCategories(categories.map(cat => 
                cat.id === id ? { ...cat, name: editName, description: editDescription } : cat
            ));

            cancelEdit();
        } catch (err) {
            console.error("Failed to update category:", err);
            alert("Failed to update category. Please try again.");
        } finally {
            setIsUpdating(false);
        }
    };

    return (
        <div>
            <h2>Inventory Categories</h2>
            
            <CategoryForm onCategoryAdded={fetchCategories} />

            {loading && <p>Loading categories...</p>}
            {error && <p style={{ color: 'red' }}>{error}</p>}
            
            {!loading && !error && (
                <ul style={{ listStyleType: 'none', padding: 0 }}>
                    {categories.map((category) => {
                        const isEditing = category.id === editingId;

                        return (
                            <li key={category.id} style={{ 
                                padding: '10px', 
                                borderBottom: '1px solid #eee', 
                                display: 'flex', 
                                alignItems: 'center',
                                justifyContent: 'space-between'
                            }}>
                                {isEditing ? (
                                    <div style={{ display: 'flex', gap: '10px', width: '70%' }}>
                                        <input 
                                            type="text" 
                                            value={editName} 
                                            onChange={(e) => setEditName(e.target.value)}
                                            style={{ padding: '5px', flex: 1 }}
                                            disabled={isUpdating}
                                        />
                                        <input 
                                            type="text" 
                                            value={editDescription} 
                                            onChange={(e) => setEditDescription(e.target.value)}
                                            placeholder="Description"
                                            style={{ padding: '5px', flex: 2 }}
                                            disabled={isUpdating}
                                        />
                                    </div>
                                ) : (
                                    <div style={{ width: '70%' }}>
                                        <strong>{category.name}</strong> 
                                        {category.description && ` - ${category.description}`}
                                    </div>
                                )}

                                <div>
                                    {isEditing ? (
                                        <>
                                            <button 
                                                onClick={() => handleUpdate(category.id)} 
                                                style={{ marginRight: '5px', color: 'green' }}
                                                disabled={isUpdating}
                                            >
                                                {isUpdating ? 'Saving...' : 'Save'}
                                            </button>
                                            <button 
                                                onClick={cancelEdit} 
                                                style={{ color: 'gray' }}
                                                disabled={isUpdating}
                                            >
                                                Cancel
                                            </button>
                                        </>
                                    ) : (
                                        <>
                                            <button 
                                                onClick={() => startEdit(category)} 
                                                style={{ marginRight: '5px', color: 'blue' }}
                                            >
                                                Edit
                                            </button>
                                            <button 
                                                onClick={() => handleDelete(category.id)} 
                                                style={{ color: 'red' }}
                                            >
                                                Delete
                                            </button>
                                        </>
                                    )}
                                </div>
                            </li>
                        );
                    })}
                </ul>
            )}
        </div>
    );
};

export default CategoryList;