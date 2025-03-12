import { useRouter } from "next/router";
import en from "../locales/en.json";
import vi from "../locales/vi.json";

const translations = { en, vi };

export function useTranslation() {
    const { locale, defaultLocale } = useRouter();
  
    // Nếu không có locale (khi ở /), dùng mặc định là "vi"
    const currentLocale = locale || defaultLocale || "vi";
    
    const t = (key) => {
      return key.split('.').reduce((o, i) => (o ? o[i] : key), translations[currentLocale]);
    };
  
    return { t, locale: currentLocale };
}
