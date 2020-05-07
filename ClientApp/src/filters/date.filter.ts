import { format } from 'date-fns';

export default (date: Date) => {
  return format(new Date(date), 'dd MMMM yyyy');
};
