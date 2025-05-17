'use client';

import React, { useState, useEffect, useRef } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { apiClient } from '@/apis/api-client';
import { getCategoriesApi } from '@/apis/admin/category';
import { useParams, useRouter } from 'next/navigation';
import { Dropdown } from 'primereact/dropdown';
import { MultiSelect } from 'primereact/multiselect';
import dynamic from 'next/dynamic';
import { Toast } from 'primereact/toast';
import { base64ToFile } from '@/functions';
import { createNewsApi, getNewsDetailApi, updateNewsApi } from '@/apis/news';

const Editor = dynamic(() => import('@/components/Editor'), { ssr: false });
const ImagePicker = dynamic(() => import('@/components/forms/ImagePicker'), { ssr: false });

interface DropdownItem {
  name: string;
  code: string;
}

interface InputValue {
  name: string;
  code: number;
}

const statusValues: Array<InputValue> = [
  { name: 'Hiển thị', code: 1 },
  { name: 'Ẩn', code: 2 }
];

const RoleAdminCreate = () => {
  const [loading1, setLoading1] = useState(false);
  const [titleVi, setTitleVi] = useState('');
  const [titleEn, setTitleEn] = useState('');
  const [multiselectValue, setMultiselectValue] = useState(null);
  const [multiselectValues, setMultiselectValues] = useState<DropdownItem[]>([]);
  const [selectStatusValue, setSelectStatusValue] = useState(statusValues[0]);
  const [initialImage, setInitialImage] = useState('');
  const [thumb, setThumb] = useState(null);
  const [contentVi, setContentVi] = useState(null);
  const [contentEn, setContentEn] = useState(null);
  const [slug, setSlug] = useState({ slug: '', viSlug: '', enSlug: '' });

  const titleViRef = useRef(null);
  const titleEnRef = useRef(null);
  const toastRef = useRef<Toast>(null);
  const categoryRef = useRef(null);
  const router = useRouter();

  const params = useParams();
  const id = params.id;

  const getData = async () => {
    const response = await apiClient.get(getNewsDetailApi(id));
    const data = response.data.data;
    // console.log(data);

    const viContent = data.contents.find((item) => item.language == 'vi');
    const enContent = data.contents.find((item) => item.language == 'en');

    setTitleVi(viContent.title);
    setTitleEn(enContent.title);

    setContentVi(JSON.parse(viContent.content));
    setContentEn(JSON.parse(enContent.content));

    const selectCategories = multiselectValues.filter((item) => data.categoryIds.includes(item.code));
    setMultiselectValue(selectCategories);

    setInitialImage(data.thumb);

    setSlug({
      slug: null,
      viSlug: viContent.slug,
      enSlug: enContent.slug
    });

    setSelectStatusValue(data.isPublished ? statusValues[0] : statusValues[1]);
  };

  const saveData = async (exit = false) => {
    if (!titleVi) {
      showError('Vui lòng nhập tiêu đề tiếng việt');
      titleViRef.current.focus();
      return;
    }
    if (!titleEn) {
      showError('Vui lòng nhập tiêu đề tiếng anh');
      titleEnRef.current.focus();
      return;
    }
    if (!multiselectValue || multiselectValue.length == 0) {
      showError('Vui lòng chọn danh mục');
      categoryRef.current.focus();
      return;
    }
    if (!thumb) {
      showError('Vui lòng chọn ảnh nền');
      return;
    }
    if (!contentVi) {
      showError('Vui lòng nhập nội dung tiếng việt');
      return;
    }
    setLoading1(true);
    const formData = new FormData();
    if (thumb) {
      const file = base64ToFile(thumb, 'upload.png');
      formData.append('Thumb', file);
    }
    if (multiselectValue) {
      const categories = multiselectValue.map((item) => item.code);
      formData.append('Categories', JSON.stringify(categories));
    }
    if (selectStatusValue) {
      formData.append('IsPublished', (selectStatusValue.code == 1).toString());
    }
    const content = [
      {
        title: titleVi,
        content: JSON.stringify(contentVi),
        language: 'vi',
        slug: slug.viSlug
      },
      {
        title: titleEn,
        content: JSON.stringify(contentEn),
        language: 'en',
        slug: slug.enSlug
      }
    ];
    formData.append('NewsContents', JSON.stringify(content));
    if (slug.slug) {
      formData.append('Slug', slug.slug);
    }
    // console.log('formData', formData.get('Slug'));

    const response = await apiClient.put(updateNewsApi(id), formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });

    setLoading1(false);

    if (response.status == 200) {
      if (exit) {
        router.push('/news');
      } else {
        showSuccess('Lưu thành công');
      }
    } else {
      showError('Có lỗi xảy ra');
    }
  };

  const getCategories = async () => {
    try {
      const response = await apiClient.get(getCategoriesApi);
      // console.log(response.data);
      const data = response.data.data;
      const categories = data.map((item) => ({
        name: item.name,
        code: item.id
      }));
      setMultiselectValues(categories);

      // setLoading1(false);
    } catch (error) {
      console.error('error', error);
    }
  };

  useEffect(() => {
    getCategories();
  }, []);

  useEffect(() => {
    if (multiselectValues.length > 0) {
      getData();
    }
  }, [multiselectValues]);

  const itemTemplate = (option) => {
    return (
      <div className="flex align-items-center">
        <span className="ml-2">{option.name}</span>
      </div>
    );
  };

  const showSuccess = (msg = 'Lưu thành công') => {
    toastRef.current?.show({
      severity: 'success',
      summary: 'Success',
      detail: msg,
      life: 3000
    });
  };

  const showError = (msg = 'Có lôi xảy ra') => {
    toastRef.current?.show({
      severity: 'error',
      summary: 'Error',
      detail: msg,
      life: 3000
    });
  };

  return (
    <div className="grid">
      <Toast ref={toastRef} />
      <div className="col-12">
        <div className="card">
          <h5>Chỉnh sửa tin tức</h5>
          <div className="p-fluid formgrid grid">
            <div className="field col-12 md:col-6">
              <label htmlFor="role_name" className="text-lg">
                Tiêu đề <span className="p-error">(*)</span>
              </label>
              <InputText ref={titleViRef} placeholder="Tiêu đề tiếng việt" id="role_name" value={titleVi} onChange={(e) => setTitleVi(e.target.value)} type="text" />
            </div>
            <div className="field col-12 md:col-6">
              <label htmlFor="role_name" className="text-lg">
                Title <span className="p-error">(*)</span>
              </label>
              <InputText ref={titleEnRef} placeholder="Tiêu đề tiếng anh" id="role_name" value={titleEn} onChange={(e) => setTitleEn(e.target.value)} type="text" />
            </div>
            <div className="field col-12">
              <div className="grid">
                <div className="field col-12 md:col-6">
                  <label htmlFor="password" className="text-lg">
                    Danh mục <span className="p-error">(*)</span>
                  </label>
                  <MultiSelect
                    value={multiselectValue}
                    onChange={(e) => setMultiselectValue(e.value)}
                    options={multiselectValues}
                    itemTemplate={itemTemplate}
                    ref={categoryRef}
                    optionLabel="name"
                    placeholder="Chọn danh mục"
                    filter
                    className="multiselect-custom"
                    display="chip"
                  />
                </div>
                <div className="field col-12 md:col-6">
                  <label htmlFor="password" className="text-lg">
                    Ảnh nền <span className="p-error">(*)</span>
                  </label>
                  <ImagePicker setImageSrc={setThumb} initialImage={initialImage} />
                </div>
              </div>
            </div>
            <div className="field col-12 md:col-6">
              <label htmlFor="address" className="text-lg">
                Nội dung{' '}
              </label>
              {contentVi && <Editor onChange={setContentVi} data={contentVi} />}
            </div>
            <div className="field col-12 md:col-6">
              <label htmlFor="address" className="text-lg">
                Content{' '}
              </label>
              {contentEn && <Editor onChange={setContentEn} data={contentEn} />}
            </div>
            <div className="field col-12 md:col-6">
              <label htmlFor="password" className="text-lg">
                Trạng thái <span className="p-error">(*)</span>
              </label>
              <Dropdown value={selectStatusValue} onChange={(e) => setSelectStatusValue(e.value)} options={statusValues} optionLabel="name" placeholder="Chọn giới tính" />
            </div>

            <div className="field col-12">
              <div className="grid mt-4 justify-content-end align-items-center gap-2">
                <div className="col-12 md:col-6 lg:col-2">
                  <Button label="Lưu và thoát" icon="pi pi-save" className="p-button-info" loading={loading1} onClick={() => saveData(true)} />
                </div>
                <div className="col-12 md:col-6 lg:col-2">
                  <Button label="Lưu" icon="pi pi-save" className="p-button-success" loading={loading1} onClick={() => saveData()} />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RoleAdminCreate;
