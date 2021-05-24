import { StatusIconPipe } from './status-icon.pipe';

describe('StatusIconPipe', () => {
  it('create an instance', () => {
    const pipe = new StatusIconPipe();
    expect(pipe).toBeTruthy();
  });

  it('Prospect should result in maybe', () =>{
    const pipe = new StatusIconPipe();
    const x = pipe.transform('Prospect');
    expect(x).toEqual('maybe');
  });
  it('prospect should result in maybe', () =>{
    const pipe = new StatusIconPipe();
    const x = pipe.transform('prospect');
    expect(x).toEqual('maybe');
  });
  it('prOspEct should result in maybe', () =>{
    const pipe = new StatusIconPipe();
    const x = pipe.transform('prOspEct');
    expect(x).toEqual('maybe');
  });

  it('customer should result in yes', () =>{
    const pipe = new StatusIconPipe();
    const x = pipe.transform('customer');
    expect(x).toEqual('yes');
  });
  it('Customer should result in yes', () =>{
    const pipe = new StatusIconPipe();
    const x = pipe.transform('Customer');
    expect(x).toEqual('yes');
  });
  it('cuStOmeR should result in yes', () =>{
    const pipe = new StatusIconPipe();
    const x = pipe.transform('cuStOmeR');
    expect(x).toEqual('yes');
  });

  it('`` (empty string) should result in whoKnows', () =>{
    const pipe = new StatusIconPipe();
    const x = pipe.transform('');
    expect(x).toEqual('whoKnows');
  });

});
