'use client';
import { Button } from 'primereact/button';
import { Column } from 'primereact/column';
import { DataTable } from 'primereact/datatable';
import React, { useEffect, useState } from 'react';
import { getBusApi, deleteBusApi } from '@/apis/admin/bus';
import { apiClient } from '@/apis/api-client';
import { useRouter } from 'next/navigation';
import { Dialog } from 'primereact/dialog';

const busTypeValues: Array<string> = ['', 'Ghế ngồi', 'Giường nằm'];

const UserAdminPage = () => {
    const [adminUsers, setAdminUsers] = useState([]);
    const [loading1, setLoading1] = useState(true);
    const [deleteLoading, setDeleteLoading] = useState(false);
    const [userSelected, setUserSelected] = useState(null);

    const router = useRouter();
    const [displayDeleteConfirmation, setDisplayDeleteConfirmation] = useState(false);

    const renderHeader1 = () => {
        return (
            <div className="flex justify-content-between">
                <Button type="button" icon="pi pi-plus" label="Thêm xe" outlined onClick={() => router.push('/admin/bus/create')} />
            </div>
        );
    };

    const getAdminUsers = async () => {
        try {
            const response = await apiClient.get(getBusApi);
            // console.log(response.data.data);
            setAdminUsers(response.data.data);

            setLoading1(false);
        } catch (error) {
            console.error('error', error);
        }
    };

    useEffect(() => {
        getAdminUsers();
    }, []);

    const handleDeleteRole = async () => {
        setDeleteLoading(true);
        const response = await apiClient.delete(deleteBusApi(userSelected.id));
        if (response.status === 200) {
            setAdminUsers(adminUsers.filter((user) => user.id !== userSelected.id));
        }
        setDisplayDeleteConfirmation(false);
        setDeleteLoading(false);
    };

    const confirmationDialogFooter = (
        <>
            <Button type="button" label="Xóa" icon="pi pi-check" loading={deleteLoading} className="p-button-danger" onClick={() => handleDeleteRole()} text />
            <Button type="button" label="Hủy" icon="pi pi-times" className="p-button-info" onClick={() => setDisplayDeleteConfirmation(false)} text autoFocus />
        </>
    );

    const deleteClickButton = async (rowData) => {
        await setUserSelected(rowData);
        setDisplayDeleteConfirmation(true);
    };

    const actionBody = (rowData: any) => {
        return (
            <div className="flex justify-content-center gap-2">
                <Button icon="pi pi-pencil" className="p-button-rounded p-button-success p-mr-2" onClick={() => router.push(`/admin/bus/${rowData.id}`)} />
                <Button icon="pi pi-trash" className="p-button-rounded p-button-danger" onClick={() => deleteClickButton(rowData)} />
            </div>
        );
    };

    const busTypeBody = (rowData: any) => {
        return <span>{busTypeValues[rowData.busType]}</span>;
    };

    const header1 = renderHeader1();

    return (
        <div className="grid">
            <div className="col-12">
                <div className="card">
                    <h5>Danh sách xe</h5>
                    <DataTable value={adminUsers} paginator className="p-datatable-gridlines" showGridlines rows={10} dataKey="id" loading={loading1} responsiveLayout="scroll" header={header1} emptyMessage="Chưa có loại xe nào">
                        <Column field="name" header="Tên xe" style={{ minWidth: '12rem' }} />
                        <Column field="brandName" header="Hãng sản xuất" style={{ minWidth: '12rem' }} />
                        <Column field="description" header="Mô tả" style={{ minWidth: '12rem' }} />
                        <Column header="Loại ghế" body={busTypeBody} style={{ minWidth: '12rem' }} />
                        <Column field="floor" header="Số tầng" className="text-center" />
                        <Column field="seatCount" header="Tổng ghế" className="text-center" />
                        <Column header="Chức năng" body={actionBody} style={{ minWidth: '12rem' }} align="center" />
                    </DataTable>
                    <Dialog header="Cảnh báo" visible={displayDeleteConfirmation} onHide={() => setDisplayDeleteConfirmation(false)} style={{ width: '350px' }} modal footer={confirmationDialogFooter}>
                        <div className="flex align-items-center justify-content-center">
                            <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                            <span>
                                Bạn chắc chắn muốn xóa xe này <b>{userSelected?.name}</b>?
                            </span>
                        </div>
                    </Dialog>
                </div>
            </div>
        </div>
    );
};

export default UserAdminPage;
