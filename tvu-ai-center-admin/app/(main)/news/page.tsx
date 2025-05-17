/* eslint-disable @next/next/no-img-element */
'use client';
import { Button } from 'primereact/button';
import { Column } from 'primereact/column';
import { DataTable } from 'primereact/datatable';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import { Toast } from 'primereact/toast';
import { Toolbar } from 'primereact/toolbar';
import React, { useEffect, useRef, useState } from 'react';
import { useRouter } from 'next/navigation';
import { Image } from 'primereact/image';
import { apiClient } from '@/apis/api-client';
import { deleteNewsApi, deleteNewsMultipleApi, getNewsListApi } from '@/apis/news';
/* @todo Used 'as any' for types here. Will fix in next version due to onSelectionChange event type issue. */
const Crud = () => {
  const [data, setData] = useState(null);
  const [deleteProductDialog, setDeleteProductDialog] = useState(false);
  const [deleteProductsDialog, setDeleteProductsDialog] = useState(false);
  const [dataSelected, setDataSelected] = useState(null);
  const [selectedDatas, setSelectedDatas] = useState(null);
  const [deleteLoading, setDeleteLoading] = useState(false);
  const [globalFilter, setGlobalFilter] = useState('');
  const toast = useRef<Toast>(null);
  const dt = useRef<DataTable<any>>(null);

  const router = useRouter();

  const getData = async () => {
    const response = await apiClient.get(getNewsListApi);
    if (response.status == 200) {
      setData(response.data.data.reverse());
      // console.log(response.data.data);
    }
  };

  useEffect(() => {
    getData();
  }, []);

  const openNew = () => {
    router.push('/news/create');
  };

  const hideDeleteProductDialog = () => {
    setDeleteProductDialog(false);
  };

  const hideDeleteProductsDialog = () => {
    setDeleteProductsDialog(false);
  };

  const editProduct = (product) => {
    router.push(`/news/${product.id}`);
  };

  const confirmDeleteProduct = (product) => {
    setDataSelected(product);
    setDeleteProductDialog(true);
  };

  const deleteData = async () => {
    setDeleteLoading(true);
    const response = await apiClient.delete(deleteNewsApi(dataSelected.id));
    if (response.status === 200 && response.data.success) {
      setData(data.filter((role) => role.id !== dataSelected.id));
    }
    toast.current?.show({
      severity: 'success',
      summary: 'Successful',
      detail: 'Delete Successfully',
      life: 3000
    });
    setDeleteProductDialog(false);
    setDeleteLoading(false);
  };

  const confirmDeleteSelected = () => {
    setDeleteProductsDialog(true);
  };

  const deleteSelectedDatas = async () => {
    setDeleteLoading(true);
    const ids = selectedDatas.map((item: any) => item.id);
    // console.log('ids', ids);
    const response = await apiClient.delete(deleteNewsMultipleApi, {
      data: ids
    });
    if (response.status === 200 && response.data.success) {
      let _datas = data.filter((val) => !selectedDatas.includes(val));
      setData(_datas);
    }
    toast.current?.show({
      severity: 'success',
      summary: 'Successful',
      detail: 'Delete Successfully',
      life: 3000
    });
    hideDeleteProductsDialog();
    setDeleteLoading(false);
  };

  const leftToolbarTemplate = () => {
    return (
      <React.Fragment>
        <div className="my-2">
          <Button label="Thêm tin tức mới" icon="pi pi-plus" severity="success" className=" mr-2" onClick={openNew} />
          <Button label="Xóa" icon="pi pi-trash" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedDatas || !(selectedDatas as any).length} />
        </div>
      </React.Fragment>
    );
  };

  const rightToolbarTemplate = () => {
    return (
      <React.Fragment>
        {/* <FileUpload mode="basic" accept="image/*" maxFileSize={1000000} chooseLabel="Import" className="mr-2 inline-block" />
                <Button label="Export" icon="pi pi-upload" severity="help" onClick={exportCSV} /> */}
      </React.Fragment>
    );
  };

  const nameBodyTemplate = (rowData) => {
    return (
      <>
        <span className="p-column-title">Title</span>
        {rowData.content.title}
      </>
    );
  };

  const imageBodyTemplate = (rowData) => {
    return (
      <>
        <span className="p-column-title">Image</span>
        <Image src={`${rowData.thumb}`} alt={'thumb'} className="shadow-2" preview width="100" />
      </>
    );
  };

  const categoryBodyTemplate = (rowData) => {
    const categories =
      rowData.categories
        .slice(0, 3)
        .map((item) => item.name)
        .join(', ') + '...';
    return (
      <>
        <span className="p-column-title">Category</span>
        {categories}
      </>
    );
  };

  const createAtBodyTemplate = (rowData) => {
    const cleanIso = rowData.createdAt.slice(0, 23); // "2025-05-14T22:22:36.326"
    const date = new Date(cleanIso);

    // Lấy các thành phần
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Tháng tính từ 0
    const year = date.getFullYear();

    const formatted = `${year}/${month}/${day} ${hours}:${minutes}`;
    return (
      <>
        <span className="p-column-title">Ngày tạo</span>
        {formatted}
      </>
    );
  };

  const publishedAtBodyTemplate = (rowData) => {
    let formatted = '';
    if (rowData.isPublished) {
      const cleanIso = rowData.createdAt.slice(0, 23); // "2025-05-14T22:22:36.326"
      const date = new Date(cleanIso);

      // Lấy các thành phần
      const hours = String(date.getHours()).padStart(2, '0');
      const minutes = String(date.getMinutes()).padStart(2, '0');
      const day = String(date.getDate()).padStart(2, '0');
      const month = String(date.getMonth() + 1).padStart(2, '0'); // Tháng tính từ 0
      const year = date.getFullYear();

      formatted = `${year}/${month}/${day} ${hours}:${minutes}`;
    }
    return (
      <>
        <span className="p-column-title">Ngày tạo</span>
        {formatted}
      </>
    );
  };

  const statusBodyTemplate = (rowData) => {
    const status = ['hidden', 'show'];
    return (
      <>
        <span className="p-column-title">Status</span>
        <span className={`product-badge status-${status[+rowData.isPublished]}`}>{status[+rowData.isPublished]}</span>
      </>
    );
  };

  const actionBodyTemplate = (rowData) => {
    return (
      <>
        <Button icon="pi pi-pencil" rounded severity="success" className="mr-2" onClick={() => editProduct(rowData)} />
        <Button icon="pi pi-trash" rounded severity="warning" onClick={() => confirmDeleteProduct(rowData)} />
      </>
    );
  };

  const header = (
    <div className="flex flex-column md:flex-row md:justify-content-between md:align-items-center">
      <h5 className="m-0">Danh sách tin tức</h5>
      <span className="block mt-2 md:mt-0 p-input-icon-left">
        <i className="pi pi-search" />
        <InputText type="search" onInput={(e) => setGlobalFilter(e.currentTarget.value)} placeholder="Search..." />
      </span>
    </div>
  );

  const deleteProductDialogFooter = (
    <>
      <Button label="Yes" loading={deleteLoading} className="p-button-danger" icon="pi pi-check" text onClick={deleteData} />
      <Button label="No" autoFocus icon="pi pi-times" text onClick={hideDeleteProductDialog} />
    </>
  );

  const deleteProductsDialogFooter = (
    <>
      <Button label="Yes" icon="pi pi-check" loading={deleteLoading} className="p-button-danger" text onClick={deleteSelectedDatas} />
      <Button label="No" autoFocus icon="pi pi-times" text onClick={hideDeleteProductsDialog} />
    </>
  );

  return (
    <div className="grid crud-demo">
      <div className="col-12">
        <div className="card">
          <Toast ref={toast} />
          <Toolbar className="mb-4" left={leftToolbarTemplate} right={rightToolbarTemplate}></Toolbar>

          <DataTable
            ref={dt}
            value={data}
            selection={selectedDatas}
            onSelectionChange={(e) => setSelectedDatas(e.value as any)}
            dataKey="id"
            paginator
            rows={10}
            rowsPerPageOptions={[5, 10, 25]}
            className="datatable-responsive"
            paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
            currentPageReportTemplate="Showing {first} to {last} of {totalRecords} products"
            globalFilter={globalFilter}
            emptyMessage="No products found."
            header={header}
            responsiveLayout="scroll"
          >
            <Column selectionMode="multiple" headerStyle={{ width: '4rem' }}></Column>
            <Column field="content.title" header="Tiêu đề" sortable body={nameBodyTemplate} headerStyle={{ minWidth: '15rem' }}></Column>
            <Column header="Ảnh nền" body={imageBodyTemplate}></Column>
            <Column field="category" header="Danh mục" sortable body={categoryBodyTemplate} headerStyle={{ minWidth: '10rem' }}></Column>
            <Column field="authorName" header="Người tạo" sortable headerStyle={{ minWidth: '10rem' }}></Column>
            <Column field="createdAt" header="Ngày tạo" sortable body={createAtBodyTemplate} headerStyle={{ minWidth: '10rem' }}></Column>
            <Column field="publishedAt" header="Ngày công khai" sortable body={publishedAtBodyTemplate} headerStyle={{ minWidth: '10rem' }}></Column>
            <Column field="isPublished" header="Trạng thái" body={statusBodyTemplate} sortable headerStyle={{ minWidth: '10rem' }}></Column>
            <Column body={actionBodyTemplate} headerStyle={{ minWidth: '10rem' }}></Column>
          </DataTable>

          <Dialog visible={deleteProductDialog} style={{ width: '450px' }} header="Confirm" modal footer={deleteProductDialogFooter} onHide={hideDeleteProductDialog}>
            <div className="flex align-items-center justify-content-center">
              <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
              {dataSelected && (
                <span>
                  Are you sure you want to delete <b>{dataSelected.content.title}</b>?
                </span>
              )}
            </div>
          </Dialog>

          <Dialog visible={deleteProductsDialog} style={{ width: '450px' }} header="Confirm" modal footer={deleteProductsDialogFooter} onHide={hideDeleteProductsDialog}>
            <div className="flex align-items-center justify-content-center">
              <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
              {selectedDatas && <span>Are you sure you want to delete the selected data?</span>}
            </div>
          </Dialog>
        </div>
      </div>
    </div>
  );
};

export default Crud;
