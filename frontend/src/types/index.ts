export interface InventoryItem {
  id: string;
  productName: string;
  quantity: number;
}

export interface HoldItem {
  productId: string;
  quantity: number;
}

export interface Hold {
  id: string;
  items: HoldItem[];
  expiry: string;
  isReleased: boolean;
}