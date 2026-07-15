import { useState, useEffect } from 'react';
import { getSuppliers, deleteSupplier, updateSupplier } from '../services/supplierService';
import SupplierForm from './SupplierForm';

const SupplierList = () => {
    const [suppliers, setSuppliers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [editingId, setEditingId] = useState(null);
    const [editName, setEditName] = useState('');
    const [editPhone, setEditPhone] = useState('');
    const [editEmail, setEditEmail] = useState('');
    const [isUpdating, setIsUpdating] = useState(false);

    const fetchSuppliers = async () => {
        setLoading(true);
        try {
            const response = await getSuppliers();
            setSuppliers(response.data);
        } catch (err) {
            setError('Failed to load suppliers.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchSuppliers();
    }, []);

    const handleDelete = async (id) => {
        if (!window.confirm("Delete this supplier?")) return;
        try {
            await deleteSupplier(id);
            setSuppliers(suppliers.filter(s => s.id !== id));
        } catch (err) {
            alert("Could not delete supplier. It may be linked to active products.");
        }
    };

    const startEdit = (sup) => {
        setEditingId(sup.id);
        setEditName(sup.name);
        setEditPhone(sup.phone);
        setEditEmail(sup.email);
    };

    const handleUpdate = async (id) => {
        if (!editName.trim() || !editPhone.trim() || !editEmail.trim()) {
            alert("All fields are required.");
            return;
        }

        setIsUpdating(true);
        try {
            // FIX: Explicitly pass 'id' in the payload to satisfy ASP.NET Core route matching
            const updatedData = { id: id, name: editName, phone: editPhone, email: editEmail };
            await updateSupplier(id, updatedData);

            setSuppliers(suppliers.map(s => 
                s.id === id ? { ...s, name: editName, phone: editPhone, email: editEmail } : s
            ));
            setEditingId(null);
        } catch (err) {
            console.error("Failed to update supplier:", err);
            alert("Failed to update supplier. Please try again.");
        } finally {
            setIsUpdating(false);
        }
    };

    return (
        <div>
            <h2>Suppliers Management</h2>
            <SupplierForm onSupplierAdded={fetchSuppliers} />
            {loading ? <p>Loading...</p> : (
                <table style={{ width: '100%', borderCollapse: 'collapse', marginTop: '10px' }}>
                    <thead>
                        <tr style={{ background: '#f4f4f4', textAlign: 'left' }}>
                            <th style={{ padding: '10px', border: '1px solid #ddd' }}>Name</th>
                            <th style={{ padding: '10px', border: '1px solid #ddd' }}>Phone</th>
                            <th style={{ padding: '10px', border: '1px solid #ddd' }}>Email</th>
                            <th style={{ padding: '10px', border: '1px solid #ddd' }}>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {suppliers.map(s => (
                            <tr key={s.id}>
                                <td style={{ padding: '10px', border: '1px solid #ddd' }}>
                                    {editingId === s.id ? <input value={editName} onChange={e => setEditName(e.target.value)} /> : s.name}
                                </td>
                                <td style={{ padding: '10px', border: '1px solid #ddd' }}>
                                    {editingId === s.id ? <input value={editPhone} onChange={e => setEditPhone(e.target.value)} /> : s.phone}
                                </td>
                                <td style={{ padding: '10px', border: '1px solid #ddd' }}>
                                    {editingId === s.id ? <input value={editEmail} onChange={e => setEditEmail(e.target.value)} /> : s.email}
                                </td>
                                <td style={{ padding: '10px', border: '1px solid #ddd' }}>
                                    {editingId === s.id ? (
                                        <>
                                            <button onClick={() => handleUpdate(s.id)} disabled={isUpdating}>Save</button>
                                            <button onClick={() => setEditingId(null)} style={{ marginLeft: '5px' }}>Cancel</button>
                                        </>
                                    ) : (
                                        <>
                                            <button onClick={() => startEdit(s)} style={{ color: 'blue' }}>Edit</button>
                                            <button onClick={() => handleDelete(s.id)} style={{ color: 'red', marginLeft: '5px' }}>Delete</button>
                                        </>
                                    )}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
};

export default SupplierList;