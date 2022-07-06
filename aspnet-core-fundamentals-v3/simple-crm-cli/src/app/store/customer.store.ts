import { Action, createAction, createReducer, props, on } from "@ngrx/store";
import { Customer } from "../customer/customer.model";
import { customerSearchCriteria, CustomerState, customerStateAdapter, initialCustomerState } from "./customer.store.model";

export const searchCustomersAction = createAction(
  '[Customer] search customers',
  props<{ criteria: customerSearchCriteria }>()
);

export const searchCustomersCompleteAction = createAction(
  '[Customer] customer search complete',
  props<{result: Customer[]}>()
);


const rawCustomerReducer = createReducer(
  initialCustomerState,
  on(searchCustomersAction, (state, props) => customerStateAdapter.removeAll({...state, criteria: props.criteria })),
  on(searchCustomersCompleteAction, (state, props) => customerStateAdapter.setAll(props.result, state))
);

export function customerReducer(state: CustomerState, action: Action) {
  return rawCustomerReducer(state, action);
};
