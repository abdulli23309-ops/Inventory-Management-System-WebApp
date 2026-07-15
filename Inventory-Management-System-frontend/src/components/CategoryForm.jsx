import { useState } from 'react';
import { createCategory } from '../services/categoryService';

const CategoryForm = ({ onCategoryAdded }) => {
    // 1. State for the input fields
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    
    // 2. State for the network request
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);

    // 3. The submission handler
    const handleSubmit = async (e) => {
        e.preventDefault(); // CRITICAL: Stops the browser from refreshing the page
        
        if (!name.trim()) {
            setError("Category name is required.");
            return;
        }

        setIsSubmitting(true);
        setError(null);

        try {
            // Construct the payload matching our backend DTO/Entity
            const newCategory = { name, description };
            
            // Call the service layer (Axios)
            await createCategory(newCategory);
            
            // Clear the form on success
            setName('');
            setDescription('');
            
            // Notify the parent component so it can refresh the list
            if (onCategoryAdded) {
                onCategoryAdded();
            }
        } catch (err) {
            console.error("Failed to create category:", err);
            setError("Failed to save category. Please try again.");
        } finally {
            setIsSubmitting(false);
        }
    };

    // 4. The UI
    return (
        <div style={{ border: '1px solid #ccc', padding: '15px', marginBottom: '20px' }}>
            <h3>Add New Category</h3>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            
            <form onSubmit={handleSubmit}>
                <div style={{ marginBottom: '10px' }}>
                    <label>Name: </label>
                    <input 
                        type="text" 
                        value={name} 
                        onChange={(e) => setName(e.target.value)} 
                        disabled={isSubmitting}
                    />
                </div>
                <div style={{ marginBottom: '10px' }}>
                    <label>Description: </label>
                    <input 
                        type="text" 
                        value={description} 
                        onChange={(e) => setDescription(e.target.value)} 
                        disabled={isSubmitting}
                    />
                </div>
                <button type="submit" disabled={isSubmitting}>
                    {isSubmitting ? 'Saving...' : 'Save Category'}
                </button>
            </form>
        </div>
    );
};

export default CategoryForm;