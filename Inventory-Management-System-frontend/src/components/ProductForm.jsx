import { useState, useEffect } from 'react';
import { createProduct } from '../services/productService';
import { getCategories } from '../services/categoryService';
import { getSuppliers } from '../services/supplierService';

const ProductForm = ({ onProductAdded }) => {
    const [name, setName] = useState('');
    const [sku, setSku] = useState('');
    const [price, setPrice] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const [supplierId, setSupplierId] = useState('');
    
    const [categories, setCategories] = useState([]);
    const [suppliers, setSuppliers] = useState([]);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        const loadDependencies = async () => {
            try {
                const [catRes, supRes] = await Promise.all([getCategories(), getSuppliers()]);
                setCategories(catRes.data);
                setSuppliers(supRes.data);
            } catch (err) {
                console.error("Failed to load form dropdown data", err);
            }
        };
        loadDependencies();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!name.trim() || !sku.trim() || !price || !categoryId || !supplierId) {
            setError("All fields are required.");
            return;
        }

        setIsSubmitting(true);
        setError(null);

        try {
            await createProduct({
                name,
                sku,
                price: parseFloat(price),
                categoryId: parseInt(categoryId),
                supplierId: parseInt(supplierId)
            });
            setName('');
            setSku('');
            setPrice('');
            setCategoryId('');
            setSupplierId('');
            if (onProductAdded) onProductAdded();
        } catch (err) {
            setError("Failed to create product.");
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div style={{ border: '1px solid #ccc', padding: '15px', marginBottom: '20px', borderRadius: '5px' }}>
            <h3>Add New Product</h3>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px', maxWidth: '400px' }}>
                <input type="text" placeholder="Product Name" value={name} onChange={e => setName(e.target.value)} disabled={isSubmitting} style={{ padding: '8px' }} />
                <input type="text" placeholder="SKU" value={sku} onChange={e => setSku(e.target.value)} disabled={isSubmitting} style={{ padding: '8px' }} />
                <input type="number" step="0.01" placeholder="Price" value={price} onChange={e => setPrice(e.target.value)} disabled={isSubmitting} style={{ padding: '8px' }} />
                
                <select value={categoryId} onChange={e => setCategoryId(e.target.value)} disabled={isSubmitting} style={{ padding: '8px' }}>
                    <option value="">-- Select Category --</option>
                    {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                </select>

                <select value={supplierId} onChange={e => setSupplierId(e.target.value)} disabled={isSubmitting} style={{ padding: '8px' }}>
                    <option value="">-- Select Supplier --</option>
                    {suppliers.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}
                </select>

                <button type="submit" disabled={isSubmitting} style={{ padding: '10px', cursor: 'pointer' }}>
                    {isSubmitting ? 'Saving...' : 'Save Product'}
                </button>
            </form>
        </div>
    );
};

export default ProductForm;