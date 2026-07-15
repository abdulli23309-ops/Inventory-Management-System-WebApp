import { useState, useEffect } from 'react';
import { getSales, createSale } from '../services/saleService';
import { getProducts } from '../services/productService';

const SaleManager = () => {
    const [sales, setSales] = useState([]);
    const [products, setProducts] = useState([]);
    const [cart, setCart] = useState([]);
    
    const [selectedProductId, setSelectedProductId] = useState('');
    const [quantity, setQuantity] = useState('');
    const [unitPrice, setUnitPrice] = useState('');

    useEffect(() => {
        const loadData = async () => {
            const [pRes, sRes] = await Promise.all([getProducts(), getSales()]);
            setProducts(pRes.data);
            setSales(sRes.data);
        };
        loadData();
    }, []);

    const addToCart = () => {
        if (!selectedProductId || !quantity || !unitPrice) {
            alert("Complete sales details.");
            return;
        }
        const product = products.find(p => p.id === parseInt(selectedProductId));
        if (product.stockQuantity < parseInt(quantity)) {
            alert(`Insufficient stock! Only ${product.stockQuantity} items are available.`);
            return;
        }
        const newItem = {
            productId: parseInt(selectedProductId),
            productName: product.name,
            quantity: parseInt(quantity),
            unitPrice: parseFloat(unitPrice)
        };
        setCart([...cart, newItem]);
        setSelectedProductId('');
        setQuantity('');
        setUnitPrice('');
    };

    const submitSale = async () => {
        if (cart.length === 0) {
            alert("No items in sales receipt.");
            return;
        }
        try {
            const payload = {
                saleDate: new Date().toISOString(),
                details: cart.map(item => ({
                    productId: item.productId,
                    quantity: item.quantity,
                    unitPrice: item.unitPrice
                }))
            };
            await createSale(payload);
            alert("Sale invoice compiled successfully!");
            setCart([]);
            const sRes = await getSales();
            setSales(sRes.data);
        } catch (err) {
            alert("Failed to submit sale.");
        }
    };

    return (
        <div>
            <h2>Sales Transactions Log</h2>
            <div style={{ border: '1px solid #ccc', padding: '15px', borderRadius: '5px', marginBottom: '20px' }}>
                <h3>New Sale Invoice</h3>
                <fieldset style={{ padding: '10px', marginBottom: '15px' }}>
                    <legend>Dispatch Items</legend>
                    <div style={{ display: 'flex', gap: '10px' }}>
                        <select value={selectedProductId} onChange={e => {
                            setSelectedProductId(e.target.value);
                            const prod = products.find(p => p.id === parseInt(e.target.value));
                            if (prod) setUnitPrice(prod.price);
                        }} style={{ padding: '5px' }}>
                            <option value="">-- Select Product --</option>
                            {products.map(p => <option key={p.id} value={p.id}>{p.name} (In stock: {p.stockQuantity})</option>)}
                        </select>
                        <input type="number" placeholder="Qty" value={quantity} onChange={e => setQuantity(e.target.value)} style={{ width: '80px', padding: '5px' }} />
                        <input type="number" placeholder="Unit Price" value={unitPrice} onChange={e => setUnitPrice(e.target.value)} style={{ width: '100px', padding: '5px' }} />
                        <button type="button" onClick={addToCart} style={{ padding: '5px 10px' }}>Add to Cart</button>
                    </div>
                </fieldset>

                {cart.length > 0 && (
                    <div>
                        <h4>Invoice Items:</h4>
                        <ul>
                            {cart.map((item, idx) => (
                                <li key={idx}>{item.productName} (Qty: {item.quantity} @ ${item.unitPrice})</li>
                            ))}
                        </ul>
                        <button onClick={submitSale} style={{ background: 'blue', color: 'white', padding: '10px 20px', cursor: 'pointer', border: 'none' }}>Register Outward Sale</button>
                    </div>
                )}
            </div>

            <h3>Invoice Archives</h3>
            <ul>
                {sales.map(s => (
                    <li key={s.id} style={{ borderBottom: '1px solid #eee', padding: '10px 0' }}>
                        <strong>Invoice #{s.id}</strong> - Date: {new Date(s.saleDate).toLocaleDateString()} | Total: ${s.totalAmount?.toFixed(2)}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default SaleManager;