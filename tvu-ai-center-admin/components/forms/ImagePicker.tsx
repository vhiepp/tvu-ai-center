'use client';

import ReactImagePickerEditor, { ImagePickerConf } from 'react-image-picker-editor';
import 'react-image-picker-editor/dist/index.css';

const ImagePicker = ({ setImageSrc, width = '100%', height = '350px' }) => {
    const config2: ImagePickerConf = {
        borderRadius: '8px',
        language: 'en',
        width,
        height,
        objectFit: 'contain',
        compressInitial: null,
        darkMode: false,
        rtl: false,
        hideAddBtn: true
    };

    const initialImage = '';

    return (
        <ReactImagePickerEditor
            config={config2}
            imageSrcProp={initialImage}
            imageChanged={(newDataUri) => {
                if (setImageSrc !== null) setImageSrc(newDataUri);
            }}
        />
    );
};

export default ImagePicker;
