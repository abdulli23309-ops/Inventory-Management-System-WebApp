import { useState, useEffect } from 'react';
import { getPurchases, createPurchase } from '../services/purchaseService';
import { getProducts } from '../services/productService';
import { getSuppliers } from '../services/supplierService';

const PurchaseManager = () => {
    const [purchases, setPurchases] = useState([]);
    const [products, setProducts] = useState([]);
    const [suppliers, setSuppliers] = useState([]);
    
    const [supplierId, setSupplierId] = useState('');
    const [cart, setCart] = useState([]);
    
    const [selectedProductId, setSelectedProductId] = useState('');
    const [quantity, setQuantity] = useState('');
    const [unitPrice, setUnitPrice] = useState('');

    useEffect(() => {
        const loadData = async () => {
            const [pRes, sRes, purRes] = await Promise.all([getProducts(), getSuppliers(), getPurchases()]);
            setProducts(pRes.data);
            setSuppliers(sRes.data);
            setPurchases(purRes.data);
        };
        loadData();
    }, []);

    const addToCart = () => {
        if (!selectedProductId || !quantity || !unitPrice) {
            alert("Please complete item details first.");
            return;
        }
        const product = products.find(p => p.id === parseInt(selectedProductId));
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

    const submitPurchase = async () => {
        if (!supplierId || cart.length === 0) {
            alert("Supplier and Cart items are required.");
            return;
        }
        try {
            const payload = {
                supplierId: parseInt(supplierId),
                purchaseDate: new Date().toISOString(),
                details: cart.map(item => ({
                    productId: item.productId,
                    quantity: item.quantity,
                    unitPrice: item.unitPrice
                }))
            };
            await createPurchase(payload);
            alert("Purchase order registered successfully!");
            setCart([]);
            setSupplierId('');
            const purRes = await getPurchases();
            setPurchases(purRes.data);
        } catch (err) {
            alert("Failed to submit purchase order.");
        }
    };

    return (
        <div>
            <h2>Purchase Orders Log</h2>
            <div style={{ border: '1px solid #ccc', padding: '15px', borderRadius: '5px', marginBottom: '20px' }}>
                <h3>New Purchase Transaction</h3>
                <label>Select Supplier: </label>
                <select value={supplierId} onChange={e => setSupplierId(e.target.value)} style={{ padding: '8px', marginBottom: '15px', display: 'block' }}>
                    <option value="">-- Choose Supplier --</option>
                    {suppliers.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}
                </select>

                <fieldset style={{ padding: '10px', marginBottom: '15px' }}>
                    <legend>Add Items</legend>
                    <div style={{ display: 'flex', gap: '10px' }}>
                        <select value={selectedProductId} onChange={e => {
                            setSelectedProductId(e.target.value);
                            const prod = products.find(p => p.id === parseInt(e.target.value));
                            if (prod) setUnitPrice(prod.price);
                        }} style={{ padding: '5px' }}>
                            <option value="">-- Select Product --</option>
                            {products.map(p => <option key={p.id} value={p.id}>{p.name}</option>)}
                        </select>
                        <input type="number" placeholder="Qty" value={quantity} onChange={e => setQuantity(e.target.value)} style={{ width: '80px', padding: '5px' }} />
                        <input type="number" placeholder="Unit Price" value={unitPrice} onChange={e => setUnitPrice(e.target.value)} style={{ width: '100px', padding: '5px' }} />
                        <button type="button" onClick={addToCart} style={{ padding: '5px 10px' }}>Add to Cart</button>
                    </div>
                </fieldset>

                {cart.length > 0 && (
                    <div>
                        <h4>Pending Order Items:</h4>
                        <ul>
                            {cart.map((item, idx) => (
                                <li key={idx}>{item.productName} (Qty: {item.quantity} @ ${item.unitPrice})</li>
                            ))}
                        </ul>
                        <button onClick={submitPurchase} style={{ background: 'green', color: 'white', padding: '10px 20px', cursor: 'pointer', border: 'none' }}>Submit Purchase Order</button>
                    </div>
                )}
            </div>

            <h3>Purchase History</h3>
            <ul>
                {purchases.map(p => (
                    <li key={p.id} style={{ borderBottom: '1px solid #eee', padding: '10px 0' }}>
                        <strong>Order #{p.id}</strong> - Date: {new Date(p.purchaseDate).toLocaleDateString()} | Total: ${p.totalAmount?.toFixed(2)}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default PurchaseManager;