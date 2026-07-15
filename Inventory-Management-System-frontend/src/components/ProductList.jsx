import { useState, useEffect } from 'react';
import { getProducts, deleteProduct } from '../services/productService';
import ProductForm from './ProductForm';

const ProductList = () => {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchProducts = async () => {
        setLoading(true);
        try {
            const response = await getProducts();
            setProducts(response.data);
        } catch (err) {
            setError('Failed to load products.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchProducts();
    }, []);

    const handleDelete = async (id) => {
        if (!window.confirm("Delete this product?")) return;
        try {
            await deleteProduct(id);
            setProducts(products.filter(p => p.id !== id));
        } catch (err) {
            alert("Could not delete product. It may be linked to purchases or sales transactions.");
        }
    };

    return (
        <div>
            <h2>Products Inventory</h2>
            <ProductForm onProductAdded={fetchProducts} />
            {loading ? <p>Loading...</p> : (
                <table style={{ width: '100%', borderCollapse: 'collapse' }}>
                    <thead>
                        <tr style={{ background: '#f4f4f4', textAlign: 'left' }}>
                            <th style={{ padding: '10px', border: '1px solid #ddd' }}>Name</th>
                            <th style={{ padding: '10px', border: '1px solid #ddd' }}>SKU</th>
                            <th style={{ padding: '10px', border: '1px solid #ddd' }}>Price</th>
                            <th style={{ padding: '10px', border: '1px solid #ddd' }}>Stock Qty</th>
                            <th style={{ padding: '10px', border: '1px solid #ddd' }}>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {products.map(p => (
                            <tr key={p.id}>
                                <td style={{ padding: '10px', border: '1px solid #ddd' }}>{p.name}</td>
                                <td style={{ padding: '10px', border: '1px solid #ddd' }}>{p.sku}</td>
                                <td style={{ padding: '10px', border: '1px solid #ddd' }}>${p.price.toFixed(2)}</td>
                                <td style={{ padding: '10px', border: '1px solid #ddd', fontWeight: 'bold' }}>{p.stockQuantity}</td>
                                <td style={{ padding: '10px', border: '1px solid #ddd' }}>
                                    <button onClick={() => handleDelete(p.id)} style={{ color: 'red' }}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
};

export default ProductList;