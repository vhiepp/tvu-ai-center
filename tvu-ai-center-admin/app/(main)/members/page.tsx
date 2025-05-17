/* eslint-disable @next/next/no-img-element */
'use client';
import { Button } from 'primereact/button';
import { Column } from 'primereact/column';
import { DataTable } from 'primereact/datatable';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import { Rating } from 'primereact/rating';
import { Toast } from 'primereact/toast';
import { Toolbar } from 'primereact/toolbar';
import React, { useEffect, useRef, useState } from 'react';
import { Demo } from '@/types';
import { useRouter } from 'next/navigation';
import { Image } from 'primereact/image';
import { apiClient } from '@/apis/api-client';
import { deleteMemberApi, deleteMemberMultipleApi, getMemberApi } from '@/apis/member';
/* @todo Used 'as any' for types here. Will fix in next version due to onSelectionChange event type issue. */
const Crud = () => {
  const [deleteLoading, setDeleteLoading] = useState(false);
  const [deleteProductDialog, setDeleteProductDialog] = useState(false);
  const [deleteProductsDialog, setDeleteProductsDialog] = useState(false);
  const [dataSelected, setDataSelected] = useState(null);
  const [selectedDatas, setSelectedDatas] = useState(null);
  const [globalFilter, setGlobalFilter] = useState('');
  const [data, setData] = useState([]);
  const toast = useRef<Toast>(null);
  const dt = useRef<DataTable<any>>(null);

  const router = useRouter();

  const getDatas = async () => {
    try {
      const res = await apiClient.get(getMemberApi);
      setData(res.data.data);
    } catch (error) {
      console.error('error', error);
    }
  };

  useEffect(() => {
    getDatas();
  }, []);

  const formatCurrency = (value: number) => {
    return value.toLocaleString('en-US', {
      style: 'currency',
      currency: 'USD'
    });
  };

  const openNew = () => {
    router.push('/members/create');
  };

  const hideDeleteProductDialog = () => {
    setDeleteProductDialog(false);
  };

  const hideDeleteProductsDialog = () => {
    setDeleteProductsDialog(false);
  };

  const confirmDeleteData = (dt) => {
    setDataSelected(dt);
    setDeleteProductDialog(true);
  };

  const deleteProduct = async () => {
    setDeleteLoading(true);
    const response = await apiClient.delete(deleteMemberApi(dataSelected.id));
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

  const findIndexById = (id: string) => {
    let index = -1;
    for (let i = 0; i < (data as any)?.length; i++) {
      if ((data as any)[i].id === id) {
        index = i;
        break;
      }
    }

    return index;
  };

  const exportCSV = () => {
    dt.current?.exportCSV();
  };

  const confirmDeleteSelected = () => {
    setDeleteProductsDialog(true);
  };

  const deleteSelectedDatas = async () => {
    setDeleteLoading(true);
    const ids = selectedDatas.map((item: any) => item.id);
    // console.log('ids', ids);
    const response = await apiClient.delete(deleteMemberMultipleApi, {
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
          <Button label="Thêm thành viên mới" icon="pi pi-plus" severity="success" className=" mr-2" onClick={openNew} />
          <Button label="Xóa" icon="pi pi-trash" severity="danger" onClick={confirmDeleteSelected} disabled={!selectedDatas || !(selectedDatas as any).length} />
        </div>
      </React.Fragment>
    );
  };

  const rightToolbarTemplate = () => {
    return (
      <React.Fragment>
        {/* <FileUpload mode="basic" accept="image/*" maxFileSize={1000000} chooseLabel="Import" className="mr-2 inline-block" /> */}
        <Button label="Export" icon="pi pi-upload" severity="help" onClick={exportCSV} />
      </React.Fragment>
    );
  };

  const nameBodyTemplate = (rowData: Demo.Product) => {
    return (
      <>
        <span className="p-column-title">Title</span>
        {rowData.fullname}
      </>
    );
  };

  const imageBodyTemplate = (rowData: Demo.Product) => {
    return (
      <>
        <span className="p-column-title">Image</span>
        <Image src={`${rowData.avatar}`} className="shadow-2" preview width="100" />
      </>
    );
  };

  const actionBodyTemplate = (rowData: Demo.Product) => {
    return (
      <>
        {/* <Button icon="pi pi-pencil" rounded severity="success" className="mr-2" onClick={() => editProduct(rowData)} /> */}
        <Button icon="pi pi-trash" rounded severity="danger" onClick={() => confirmDeleteData(rowData)} />
      </>
    );
  };

  const header = (
    <div className="flex flex-column md:flex-row md:justify-content-between md:align-items-center">
      <h5 className="m-0">Danh sách thành viên</h5>
      <span className="block mt-2 md:mt-0 p-input-icon-left">
        <i className="pi pi-search" />
        <InputText type="search" onInput={(e) => setGlobalFilter(e.currentTarget.value)} placeholder="Search..." />
      </span>
    </div>
  );

  const deleteProductDialogFooter = (
    <>
      <Button label="Yes" loading={deleteLoading} className="p-button-danger" icon="pi pi-check" text onClick={deleteProduct} />
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
            <Column header="Ảnh đại diện" body={imageBodyTemplate}></Column>
            <Column field="fullname" header="Họ tên" sortable body={nameBodyTemplate} headerStyle={{ minWidth: '15rem' }}></Column>
            <Column field="position" header="Chức vụ" sortable headerStyle={{ minWidth: '15rem' }}></Column>
            <Column field="skills" header="Chuyên môn" sortable headerStyle={{ minWidth: '15rem' }}></Column>
            <Column field="email" header="Email" sortable headerStyle={{ minWidth: '15rem' }}></Column>
            <Column field="phoneNumber" header="Số điện thoại" sortable headerStyle={{ minWidth: '15rem' }}></Column>
            <Column body={actionBodyTemplate} headerStyle={{ minWidth: '10rem' }}></Column>
          </DataTable>

          {/* <Dialog visible={productDialog} style={{ width: '450px' }} header="Product Details" modal className="p-fluid" footer={productDialogFooter} onHide={hideDialog}>
            {product && product.image && <img src={`${product.image}`} alt={product.image} width="150" className="mt-0 mx-auto mb-5 block shadow-2" />}
            <div className="field">
              <label htmlFor="name">Tiêu đề</label>
              <InputText
                id="name"
                value={product?.name}
                onChange={(e) => onInputChange(e, 'name')}
                required
                autoFocus
                className={classNames({
                  'p-invalid': submitted && !product.name
                })}
              />
              {submitted && !product.name && <small className="p-invalid">Name is required.</small>}
            </div>
            <div className="field">
              <label htmlFor="description">Description</label>
              <InputTextarea id="description" value={product.description} onChange={(e) => onInputChange(e, 'description')} required rows={3} cols={20} />
            </div>

            <div className="field">
              <label className="mb-3">Category</label>
              <div className="formgrid grid">
                <div className="field-radiobutton col-6">
                  <RadioButton inputId="category1" name="category" value="Accessories" onChange={onCategoryChange} checked={product.category === 'Accessories'} />
                  <label htmlFor="category1">Accessories</label>
                </div>
                <div className="field-radiobutton col-6">
                  <RadioButton inputId="category2" name="category" value="Clothing" onChange={onCategoryChange} checked={product.category === 'Clothing'} />
                  <label htmlFor="category2">Clothing</label>
                </div>
                <div className="field-radiobutton col-6">
                  <RadioButton inputId="category3" name="category" value="Electronics" onChange={onCategoryChange} checked={product.category === 'Electronics'} />
                  <label htmlFor="category3">Electronics</label>
                </div>
                <div className="field-radiobutton col-6">
                  <RadioButton inputId="category4" name="category" value="Fitness" onChange={onCategoryChange} checked={product.category === 'Fitness'} />
                  <label htmlFor="category4">Fitness</label>
                </div>
              </div>
            </div>

            <div className="formgrid grid">
              <div className="field col">
                <label htmlFor="price">Price</label>
                <InputNumber id="price" value={product.price} onValueChange={(e) => onInputNumberChange(e, 'price')} mode="currency" currency="USD" locale="en-US" />
              </div>
              <div className="field col">
                <label htmlFor="quantity">Quantity</label>
                <InputNumber id="quantity" value={product.quantity} onValueChange={(e) => onInputNumberChange(e, 'quantity')} />
              </div>
            </div>
          </Dialog> */}

          <Dialog visible={deleteProductDialog} style={{ width: '450px' }} header="Confirm" modal footer={deleteProductDialogFooter} onHide={hideDeleteProductDialog}>
            <div className="flex align-items-center justify-content-center">
              <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
              {dataSelected && (
                <span>
                  Are you sure you want to delete <b>{dataSelected.fullname}</b>?
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
