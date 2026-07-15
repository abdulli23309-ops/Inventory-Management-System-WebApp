import { useState, useEffect } from 'react';
import { getInventoryValuation } from '../services/reportService';

const Reports = () => {
    const [report, setReport] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchReportData = async () => {
            try {
                const response = await getInventoryValuation();
                setReport(response.data);
            } catch (err) {
                setError("Failed to generate real-time reports.");
            } finally {
                setLoading(false);
            }
        };
        fetchReportData();
    }, []);

    return (
        <div>
            <h2>Real-time Executive Reports</h2>
            {loading ? <p>Calculating reports...</p> : error ? <p style={{ color: 'red' }}>{error}</p> : (
                <div style={{ background: '#fcfcfc', border: '2px solid #0056b3', borderRadius: '8px', padding: '25px', display: 'inline-block' }}>
                    <h3 style={{ color: '#0056b3', marginTop: 0 }}>Active Inventory Asset Valuation</h3>
                    <p style={{ fontSize: '1.2rem' }}>Calculated dynamically via optimized Dapper database queries.</p>
                    <div style={{ fontSize: '2.5rem', fontWeight: 'bold', color: '#28a745', marginTop: '15px' }}>
                        ${report?.totalValuation?.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) || '0.00'}
                    </div>
                </div>
            )}
        </div>
    );
};

export default Reports;