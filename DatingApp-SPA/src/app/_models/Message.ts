export interface Message {
  id: number;
  senderid: number;
  recipientId: number;
  recipientPhotoUrl: string;
  senderPhotoUrl: string;
  senderKnownAs: string;
  recipientKnownAs: string;
  context: string;
  isRead: boolean;
  messageRead: Date;
  messageSent: Date;

}
