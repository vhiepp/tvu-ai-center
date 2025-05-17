'use client';

import ReactImagePickerEditor, { ImagePickerConf } from 'react-image-picker-editor';
import 'react-image-picker-editor/dist/index.css';

const ImagePicker = ({ setImageSrc, width = '100%', height = '350px', objectFit = 'contain', initialImage = '' }) => {
  const config2: ImagePickerConf = {
    borderRadius: '8px',
    language: 'en',
    width,
    height,
    // @ts-ignore
    objectFit,
    compressInitial: null,
    darkMode: false,
    rtl: false,
    hideAddBtn: true
  };

  return (
    <div>
      <ReactImagePickerEditor
        config={config2}
        imageSrcProp={initialImage}
        imageChanged={(newDataUri) => {
          if (setImageSrc !== null) setImageSrc(newDataUri);
        }}
      />
    </div>
  );
};

export default ImagePicker;
