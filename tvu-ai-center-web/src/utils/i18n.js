import { useRouter } from "next/router";
import en from "../locales/en.json";
import vi from "../locales/vi.json";
import vn_img from "@/images/icon/vn.png";
import en_img from "@/images/icon/en.png";

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

export const languages = [
  {
    code: "en",
    name: "English",
    icon_img: vn_img
  },
  {
    code: "vi",
    name: "Tiếng Việt",
    icon_img: en_img
  },
];

export function getCurrentLocale() {
  if (typeof window !== "undefined") {
    const pathLocale = window.location.pathname.split("/")[1];
    return pathLocale === "en" ? "en" : "vi"; // Mặc định là "vi"
  }

  return "vi"; // Trả về mặc định nếu chạy trên server
}

export function getCurrentLocaleName() {
  if (typeof window !== "undefined") {
    const pathLocale = window.location.pathname.split("/")[1];
    return pathLocale === "en" ? "English" : "Tiếng Việt"; // Mặc định là "Tiếng Việt"
  }
  
  return "Tiếng Việt"; // Trả về mặc định nếu chạy trên server
}